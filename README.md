# ChatBot

## Description

This is a simple chatbot that uses a command-based solution to fetch stock data from [Stooq API](https://stooq.com/q/l/).

It has a SignalR chatroom implemented in a Blazor Server App, a Bot Worker that processes commands, and uses RabbitMQ to send/receive messages. SQL Server is used to persist messages and user data.

---

## Features

- Authentication using IdentityServer
- Chatroom using SignalR
- `/stock=stock_code` command to get stock data
- Messages ordered by timestamp descending
- Shows the latest 50 messages in the chatroom
- Dockerized with Docker Compose for easy setup

---

## Built With

- Docker & Docker Compose
- .NET 8
- Blazor Server
- SignalR
- RabbitMQ
- SQL Server 2022
- CsvHelper
- ASP Net Core Identity

---

## Prerequisites

- Docker & Docker Compose  

or  

- .NET 8 Runtime
- Microsoft SQL Server
- RabbitMQ

---

## Installation

### Docker (Recommended)

1. From the root folder, build and start all services:

```bash
docker-compose up -d --build
```

## Access services:

Chat App: http://localhost:8080

RabbitMQ Management UI: http://localhost:15672

User: chatbot / Password: chatbot123
