# .Net Microservices

## Prerequisites
- Docker desktop with Kubernetes
- Pg-Admin Client for postgresql databases

## How to run project
From the root of each project run
- PatformService:  `docker build -f PlatformService.Api/Dockerfile -t nanaessandoh/platformservice .`
- CommandService: `docker build -f CommandService.Api/Dockerfile -t nanaessandoh/commandservice .`

Push images to docker hub (Must have a docker account)
- PlatformService: `docker push replace_with_your_docker_usename/platformservice`
- CommandService: `docker push replace_with_your_docker_username/commandservice`

Spin up Postgresql in Kubernetes cluster
From the kubernetes folder. run
- This will set up your persistence volume claim and spin a pod with postgresql
- `kubectl apply -f postgresql-deploy`
- Download pg-admin
- Run `kubectl get services` (take note of external ip for the loadbalancer of postgresql server and port
 - In pg-admin hostname is external ip (localhost in my case, user: postgres, password: password set in deployment .yaml file (myPassword in my instance)
 - Safe way is to store password in kubernetes secrets.

Spin up Ingres NginX loadbalancer
- This pod spin up a container that enables you to externally communicate with the services (pods) you have created.
- `kubectl apply -f ingress-server`

Spin up RabbitMQ message bus service
- This pod spin up a container that enables asynchronous message broking between the platform and command service.
- `kubectl apply -f rabbitmq-deploy`
- Visit `http://localhost:15672/` to access the management interface
- Username: guest Password: guest

Deloy images into Kubernetes cluster
From the kubernetes folder. run
- PlatformService: `kubectl apply -f platforms-deploy`
- CommandService: `kubectl apply -f commands-deploy`

Deloy images into Kubernetes cluster
From the kubernetes folder. run
- PlatformService: `kubectl apply -f platforms-deploy`
- CommandService: `kubectl apply -f commands-deploy`

## Other Useful Command
- Spin images up in local dev (-d is optional)
- `docker run -p 8080:80 -d nanaessandoh/platformservice`
- `docker run -p 8080:80 -d nanaessandoh/commandservice`
- See running containers `docker ps`
- Start container `docker start <container-id form docker ps>`
- Stop container `docker stop <container-id from docker ps>`
- Restart a deployment `kubectl rollout restart deployment <deploy name from kubectl get deployments>`
- Get applied deployments `kubectl get deployment`
- Get runnning Pods `kubectl get pods`
- See running Service `kubectl get services`
- Delete a deployment `kubectl delete deployment <deployment name>`
- Delete a pod `kubectl delete pod <pod name>`
- Delete a deployment `kubectl delete service <service name>`

## Glossary
- Cluster: Contains a set of "worker machines" called Nodes. Every cluster has at least 1 node.
- Cluster IP: A Service that exposes the container "internally" within the Cluster.
- Container: An image that has been executed, containing the app and it's dependencies.
- Image: The result of building an app and it's dependencies. Images are transferable units.
- Load Balancer: A Service that exposes a container externally.
- Node: A Node is "worker machine" that runs containerized applications.
- Node Port: A Service used for development purposes to expose containers externally.
- Pod: Smallest K8S object. Represents a set of running containers.