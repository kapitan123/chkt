Solution is based on Dapr framework and it's reference projects

MerchantPayment.API
	- Public API
	- Performs Basic Validation of input parameters
	- Uses value of "X-MERCHANT-KEY" to authenticate requests
		- CredentialKeysRepo mocks external storage 
	- CardDetails.Message is used for testing of mocks

BankProxy.API
	- Proxy for bank requests. Uses two mocked banks. 
	- All cards starting with 000000 belongs to one, the rest to other.
	- We assume that we have same responses from both banks. So we don't need to add mappings.
	- /checkout is idempotent in case of message duplication

Most of state stores are mocked by in memory collections, so they can't have replicas
Some Entities and DTO are not separated 
BankProxy is idempotent
