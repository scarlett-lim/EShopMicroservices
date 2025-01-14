# EShopMicroservices

Catalog API
- Using ASP.NET Core Minimal APIs and latest features of .NET8 and C# 12
- Vertical Slice Architecture implementation with Feature folders and single .cs file includes different classes in one file
- CQRS implementation using MediatR library
- CQRS Validation Pipeline Behaviors with MediatR and FluentValidation
- Use Marten library for .NET Transactional Document DB on PostgreSQL, so Product microservices database will be PostgreSQL but acting as a Document DB using Marten library
- Use Carter for Minimal API endpoint definition
- Cross-cutting concerns Logging, Global Exception Handling and Health Checks
- Implement Dockerfile and docker-compose file for running Product microservice and PostgreSQL database in Docker environment
