Solution requires minikube or docker-for-desktop with kubernetes support and helm

Install: Helm https://helm.sh/docs/intro/install/
Install: Dapr CLI https://docs.dapr.io/getting-started/install-dapr-cli/
Install: kubectl https://kubernetes.io/docs/tasks/tools/

1) Run: `$ dapr init -k`

2) Go to: deploy\helm 
	Run: `$ helm install payments-test .`

3) Teardown after tests
	Run: `$ helm uninstall payments-test`

4) Use Postman collection or call `http://localhost:80`
	Health UI at `http://localhost:30007/healthchecks-ui`

- Update images
docker build -f src\BankProxy\BankProxy.API\Dockerfile -t kapitan123/bank-proxy.api .; docker push kapitan123/bank-proxy.api
docker build -f src\MerchantPayment\MerchantPayment.API\Dockerfile -t kapitan123/merchant-payment.api .; docker push kapitan123/merchant-payment.api



