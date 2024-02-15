# Introduction 
This is solution for Coding Assessment (.Net 6 REST API, Repository - PostgreSQL/MongoDB, UI - Vue 3).
1. WebApi - project to process some call events.
2. WebApi.Test - project with unit tests.
3. vue-project - ui client on Vue 3.

# Getting Started. Backend
1. Run docker-compose.yaml to setup postgres db on localhost.

# Build and Test
1. Build WebApi and WebApi.Test project.
2. Run WebApi.Test unit tests to check the state.
3. Run WebApi
   1. Use Swagger, which part of this solution (or use Postman)
   2. Execute several POST request to generate some data (for Guid creation you can use Guid Generator tool)
   3. Call GET to see all generated data.

# Getting Started. UI client
1. Change the directory to vue-project
2. Run command
```sh
npm install
```
# Compile and Run
1. Run command
```sh
npm run dev
```
