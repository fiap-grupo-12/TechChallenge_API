provider "aws" {
  region = "us-east-1"
}

terraform {
  backend "s3" {
    bucket = "terraform-tfstate-grupo12-fiap-2024"
    key    = "api/terraform.tfstate"
    region = "us-east-1"
  }
}

data "aws_caller_identity" "current" {}
data "aws_vpc" "default" {
  default = true
}
data "aws_ecs_cluster" "main" {
  cluster_name = "ecs-cluster"
}
data "aws_lb_target_group" "ecs_tg" {
  name = "ecs-tg"
}
data "aws_subnets" "default" {
  filter {
    name   = "vpc-id"
    values = [data.aws_vpc.default.id]
  }
}

# Security Group para o ECS
resource "aws_security_group" "ecs_sg" {
  name        = "ecs-tasks-sg"
  description = "Permitir acesso ao ECS via Load Balancer"
  vpc_id      = data.aws_vpc.default.id

  ingress {
    protocol    = "tcp"
    from_port   = 8080
    to_port     = 8080
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    protocol    = "-1"
    from_port   = 0
    to_port     = 0
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# Função de execução do ECS (IAM Role)
resource "aws_iam_role" "ecs_task_execution_role" {
  name = "ecs_task_execution_role"
  assume_role_policy = jsonencode({
    Version = "2012-10-17"
    Statement = [{
      Action = "sts:AssumeRole"
      Effect = "Allow"
      Principal = {
        Service = "ecs-tasks.amazonaws.com"
      }
    }]
  })
}

# Política IAM para acessar Secrets Manager e RDS
resource "aws_iam_policy" "ecs_task_policy" {
  name        = "ecs_task_policy"
  description = "Permite que o ECS acesse o Secrets Manager e o RDS"

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Effect = "Allow",
        Action = [
          "secretsmanager:*",
				  "rds:*",
				  "kms:*",
				  "ecs:*",
				  "ssmmessages:*",
				  "logs:*",
				  "cloudwatch:*"
        ],
        Resource = "*"
      }
    ]
  })
}

# Vincular política ao role da execução
resource "aws_iam_role_policy_attachment" "ecs_task_execution_role_policy_attachment" {
  role       = aws_iam_role.ecs_task_execution_role.name
  policy_arn = aws_iam_policy.ecs_task_policy.arn
}

# Task Definition para o ECS
resource "aws_ecs_task_definition" "app" {
  family                   = "ecs-task"
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                      = "256"
  memory                   = "512"
  execution_role_arn       = aws_iam_role.ecs_task_execution_role.arn

  container_definitions = jsonencode([
    {
      name      = "dotnet-app"
      image     = "${data.aws_caller_identity.current.account_id}.dkr.ecr.us-east-1.amazonaws.com/techchallenge_api:latest"
      cpu       = 256
      memory    = 512
      essential = true
      portMappings = [
        {
          containerPort = 8080
          hostPort      = 8080
        }
      ]
    }
  ])
}

# Serviço ECS
resource "aws_ecs_service" "app" {
  name            = "ecs-service"
  cluster         = data.aws_ecs_cluster.main.id
  task_definition = aws_ecs_task_definition.app.arn
  desired_count   = 1
  launch_type     = "FARGATE"
  network_configuration {
    subnets          = data.aws_subnets.default.ids
    security_groups  = [aws_security_group.ecs_sg.id]
    assign_public_ip = true
  }
  load_balancer {
    target_group_arn = data.aws_lb_target_group.ecs_tg.arn
    container_name   = "dotnet-app"
    container_port   = 8080
  }
}

# Attach policy to allow ECS tasks to pull images from ECR
resource "aws_iam_role_policy_attachment" "ecs_task_execution_role_policy" {
  role       = aws_iam_role.ecs_task_execution_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
}
