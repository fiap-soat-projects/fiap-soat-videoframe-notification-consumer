# 📬 VideoFrame Notification Consumer

Este repositório contém o **consumer de notificações** desenvolvido para o hackathon FIAP SOAT. O serviço é um **Worker Service** que consome mensagens de um tópico Kafka e realiza notificações via **e-mail (AWS SES)** ou **webhook HTTP** sobre o resultado (sucesso/erro) do processamento de edições de vídeos realizado pelo microsserviço [videoframe-processor](https://github.com/fiap-soat-projects).

## 🏃 Integrantes do grupo

- **Jeferson dos Santos Gomes** – RM 362669
- **Jamison dos Santos Gomes** – RM 362671
- **Alison da Silva Cruz** – RM 362628

## 👨‍💻 Tecnologias Utilizadas

- **.NET 10 (C# 14)** – Worker Service construído com `Microsoft.Extensions.Hosting`
- **Apache Kafka** – mensageria / fila de eventos
- **MongoDB** – banco de dados NoSQL para persistência de notificações e logs
- **Amazon SES (Simple Email Service)** – envio de e-mails
- **Docker & Docker Compose** – containerização e orquestração local
- **Prometheus / Promtail** – métricas
- **Grafana / Loki** – monitoramento
- **SonarCloud** – análise de qualidade de código

## 🏗️ Arquitetura do Projeto

O projeto segue uma arquitetura em camadas inspirada em Clean Architecture:

```
src/
├── Domain/              # Entidades, enums, value objects e use cases
├── Adapter/             # Aplicações, gateways (senders, repositories), factories e mensagens
└── Drivers/
    ├── Consumer/        # Worker Service (ponto de entrada da aplicação)
    └── Infrastructure/  # Conexões (Mongo, Kafka), repositórios MongoDB e providers
```

## ⚙️ Fluxo de Funcionamento

1. O **Worker** inicia e se inscreve no tópico Kafka configurado.
2. Ao consumir uma mensagem (`NotificationMessage`), o serviço processa a notificação.
3. Para cada **target** da notificação, o sistema resolve o canal apropriado:
   - `Email` → envia via **AWS SES**
   - `Webhook` → realiza um **POST HTTP** para a URL informada
4. O resultado de cada envio é registrado como **NotificationLog** no MongoDB.

## 🔌 Canais de Notificação

| Canal     | Descrição                                       |
|-----------|--------------------------------------------------|
| `Email`   | Envio de e-mail via Amazon Simple Email Service  |
| `Webhook` | POST HTTP com payload JSON para a URL de destino |

## 🌐 Variáveis de Ambiente

A aplicação utiliza as seguintes variáveis para configuração:

| Variável                                            | Descrição                                                                 |
|-----------------------------------------------------|---------------------------------------------------------------------------|
| `KAFKA_HOST`                                        | Endereço do broker Kafka                                                  |
| `KAFKA_CONSUMER_GROUP`                              | Nome do grupo de consumidores Kafka                                       |
| `NOTIFICATION_TOPIC_NAME`                           | Nome do tópico Kafka de onde as mensagens são consumidas                  |
| `VIDEOFRAME_NOTIFICATION_MONGODB_CONNECTION_STRING` | String de conexão para o banco MongoDB                                    |
| `APP_NAME`                                          | Nome da aplicação (usado na conexão MongoDB)                              |
| `EMAIL_SENDER`                                      | Endereço de e-mail remetente para notificações via AWS SES                |
| `AWS_ACCESS_KEY_ID`                                 | Chave de acesso da conta ou role AWS                                      |
| `AWS_SECRET_ACCESS_KEY`                             | Segredo correspondente à `AWS_ACCESS_KEY_ID`                              |
| `AWS_REGION`                                        | Região dos recursos AWS (ex.: `us-east-1`)                                |

## 🗃️ Banco de Dados

O serviço utiliza **MongoDB** para persistência. As principais coleções seguem as estruturas:

### Notification

| Campo                 | Tipo                         |
|-----------------------|------------------------------|
| Id                    | string                       |
| CreatedAt             | datetime                     |
| UpdatedAt             | datetime/null                |
| EditId                | string                       |
| UserId                | string                       |
| UserName              | string                       |
| FileUrl               | string                       |
| Type                  | NotificationType             |
| Error                 | string/null                  |
| NotificationTargets   | List\<NotificationTarget\>   |

### NotificationLog

| Campo            | Tipo                |
|------------------|---------------------|
| Id               | string              |
| CreatedAt        | datetime            |
| UpdatedAt        | datetime/null       |
| NotificationId   | string              |
| Target           | NotificationTarget  |
| Status           | NotificationStatus  |
| Error            | string/null         |

### Enums

- **NotificationType**: `Success`, `Error`
- **NotificationStatus**: `Pending`, `Sent`, `Failed`
- **NotificationChannel**: `Webhook`, `Email`

## 📨 Formato da Mensagem Kafka

```json
{
  "editId": "abc-123",
  "userId": "user-456",
  "userName": "João Silva",
  "fileUrl": "https://bucket.s3.amazonaws.com/video.mp4",
  "type": Sucess,
  "error": null,
  "notificationTargets": [
    { "channel": "Email", "target": "usuario@email.com" },
    { "channel": "Webhook", "target": "https://meu-webhook.com/notify" }
  ]
}
```

## 👤 Convenções

- Mensagens consumidas e payloads de webhook utilizam **JSON**.
- Configurações de ambiente estão em `appsettings.json` / `appsettings.Development.json`.
- O projeto utiliza **Clean Architecture** com separação clara entre Domain, Adapter e Drivers.
- Análise de cobertura e qualidade via **SonarCloud**.

## 🚀 Como Executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Apache Kafka em execução
- MongoDB em execução
- Credenciais AWS configuradas (para envio de e-mail via SES)

### Executando localmente

```bash
# Restaurar dependências
dotnet restore

# Compilar
dotnet build --configuration Release

# Definir variáveis de ambiente (PowerShell)
$env:KAFKA_HOST = "localhost:9092"
$env:KAFKA_CONSUMER_GROUP = "notification-group"
$env:NOTIFICATION_TOPIC_NAME = "notification-topic"
$env:VIDEOFRAME_NOTIFICATION_MONGODB_CONNECTION_STRING = "mongodb://localhost:27017"
$env:APP_NAME = "videoframe-notification-consumer"
$env:EMAIL_SENDER = "noreply@example.com"
$env:AWS_ACCESS_KEY_ID = "your-access-key"
$env:AWS_SECRET_ACCESS_KEY = "your-secret-key"
$env:AWS_REGION = "us-east-1"

# Executar
dotnet run --project src/Drivers/Consumer
```

### Executando testes

```bash
dotnet test --configuration Release --verbosity normal
```