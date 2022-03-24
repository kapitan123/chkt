# Solution structure
The solution is based on Dapr framework and its reference projects
Most of the state stores are mocked by in-memory collections, so they can't have actual replicas

### MerchantPayment.API

- Public API
- Performs Basic Validation of input parameters
- Uses value of `X-MERCHANT-KEY` header to authenticate requests
	- Test key value is `34f25424-088c-482a-a75e-8ccbbecf8112`
	- `MerchantKeysRepositoryMock` mocks external storage 
- `CardDetails.Message` is used for testing of mocks

### BankProxy.API

- Proxy for bank requests. Uses two mock banks. 
- All cards starting with 000000 belong to one, the rest to the other.
- We assume that we have the same responses from both banks. So we don't need to add mappings.
- Is idempotent in case of message duplication