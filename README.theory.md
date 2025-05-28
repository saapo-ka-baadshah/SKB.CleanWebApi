# SKB.CleanWebApi.Theory

This template provides us a partial CleanWebApi Template. With this partial
Clean WebApi we address following layers of the Clean Architecture.
- Presentation Layer
- Application Layer
- Domain Layer

With this we allow following designing patterns to be the part of the
service:
1. Command Query Responsibility Segregation (CQRS)
2. Dependency Injection
3. Domain Driven Designs

## CQRS ✅
To implement CQRS, we use MediatR. This allows us to create a contract bounded
`Command` or `Queries`.

```csharp
record SomeQuery(int id, string value)
	: IRequest<SomeResponseType>;

---

class SomeQueryHandler: IRequestHandler<SomeQuery, SomeResponseType>
{
	Task<SomeResponseType> Handle(SomeQuery query, CancellationToken cToken)
	{
		// Perform the operations here
	}
}

---

var query = new SomeQuery(5, "RandomValue");
IMediatr mediatr = builder.services.GetService<IMediatr>();
// Usually the mediatr is injected with the Dependency injection patterns
mediatr.Send(query);
```
We implement the CQRS to resolve the Transients while handling higher traffic.


