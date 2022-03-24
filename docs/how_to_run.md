# Deplyment
Solution requires minikube or docker-for-desktop with kubernetes support and helm

## Install: 
- Helm https://helm.sh/docs/intro/install/
- Dapr CLI https://docs.dapr.io/getting-started/install-dapr-cli/
- kubectl https://kubernetes.io/docs/tasks/tools/

## Run
1) Run: `$ dapr init -k`

2) Go to: deploy\helm 
	Run: `$ helm install payments-test .`

3) Teardown after tests
	Run: `$ helm uninstall payments-test`

4) Use Postman collection or call `http://localhost:80`
	Health UI at `http://localhost:30007/healthchecks-ui`