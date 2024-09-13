//Define a regiao e credenciais da AWS 
provider "aws" {
  region = "us-east-1"
}

//Busca a VPC e Sub-Net padroes da AWS
data "aws_vpc" "default" {
  default = true
}

data "aws_subnet_ids" "default" {
  vpc_id = "${data.aws_vpc.default.id}"
}

//Cria Security Group para o LoadBalancer e ECS
resource "aws_security_group" "lb" {
  name = "lb-sg"
  description = "Controle de acesso ao Application Load Balancer (ALB)"

  ingress {
    protocol = "tcp"
    from_port = 80
    to_port = 80
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    protocol = "-1"
    from_port = 0
    to_port = 0
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_security_group" "ecs_tasks" {
  name = "ecs-tasks-sg"
  description = "Permitir acesso ao ECS apenas via Load Balancer"

  ingress {
    protocol = "tcp"
    from_port = 4000
    to_port = 4000
    cidr_blocks = ["0.0.0.0/0"]
    security_groups = [aws_security_group.lb.id]
  }

  egress {
    protocol = "-1"
    from_port = 0
    to_port = 0
    cidr_blocks = ["0.0.0.0/0"]
  }
}

//Criacao do ECR
resource "aws_ecr_repository" "repo" {
  name = "TechChallenge/API"
}

resource "aws_ecr_lifecycle_policy" "repo-policy" {
  repository = aws_ecr_repository.repo.name

  policy = <<EOF
{
  "rules": [
    {
      "rulePriority": 1,
      "description": "Manter a imagem implantada com a tag mais recente",
      "selection": {
        "tagStatus": "tagged",
        "tagPrefixList": ["latest"],
        "countType": "imageCountMoreThan",
        "countNumber": 1
      },
      "action": {
        "type": "expire"
      }
    },
    {
      "rulePriority": 2,
      "description": "Manter as últimas 2 imagens",
      "selection": {
        "tagStatus": "any",
        "countType": "imageCountMoreThan",
        "countNumber": 2
      },
      "action": {
        "type": "expire"
      }
    }
  ]
}
EOF
}

//IAM
data "aws_iam_policy_document" "ecs_task_execution_role" {
  version = "2012-10-17"
  statement {
    sid = ""
    effect = "Allow"
    actions = ["sts:AssumeRole"]

    principals {
      type = "Service"
      identifiers = ["ecs-tasks.amazonaws.com"]
    }
  }
}

resource "aws_iam_role" "ecs_task_execution_role" {
  name = "ecs-staging-execution-role"
  assume_role_policy = data.aws_iam_policy_document.ecs_task_execution_role.json
}

resource "aws_iam_role_policy_attachment" "ecs_task_execution_role" {
  role = aws_iam_role.ecs_task_execution_role.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AmazonECSTaskExecutionRolePolicy"
}

//Importar Task Definition
data "template_file" "sproutlyapp" {
  template = file("./TaskDefinition.json")
  vars = {
    aws_ecr_repository = aws_ecr_repository.repo.repository_url
    tag = "latest"
    app_port = 80
  }
}

resource "aws_ecs_task_definition" "service" {
  family = "techchallenge-api"
  network_mode = "awsvpc"
  execution_role_arn = aws_iam_role.ecs_task_execution_role.arn
  cpu = 256
  memory = 2048
  requires_compatibilities = ["FARGATE"]
  container_definitions = data.template_file.sproutlyapp.rendered
  tags = {
    Environment = "staging"
    Application = "TechChallenge-API"
  }
}

//ECS
resource "aws_ecs_service" "staging" {
  name = "staging"
  cluster = aws_ecs_cluster.staging.id
  task_definition = aws_ecs_task_definition.service.arn
  desired_count = 1
  launch_type = "FARGATE"

  network_configuration {
    security_groups = [aws_security_group.ecs_tasks.id]
    subnets = data.aws_subnet_ids.default.ids
    assign_public_ip = true
  }

  load_balancer {
    target_group_arn = aws_lb_target_group.staging.arn
    container_name = "cloud-techchallenge"
    container_port = 4000
  }

  depends_on = [aws_lb_listener.https_forward, aws_iam_role_policy_attachment.ecs_task_execution_role]

  tags = {
    Environment = "staging"
    Application = "sproutlyapi"
  }
}

//Cloud watch
resource "aws_cloudwatch_log_group" "TechChallenge-API" {
  name = "awslogs-TechChallenge-API-staging"

  tags = {
    Environment = "staging"
    Application = "TechChallenge-API"
  }
}