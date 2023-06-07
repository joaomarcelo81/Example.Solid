# SOLID Principles

This repository demonstrates the SOLID principles in object-oriented software design. The SOLID principles provide guidelines for designing maintainable and flexible software systems. Each principle focuses on a specific aspect of software design and aims to improve code quality, modularity, and extensibility.

## Table of Contents

- [SOLID Principles Overview](#solid-principles-overview)
- [S - Single Responsibility Principle (SRP)](#s---single-responsibility-principle-srp)
- [O - Open/Closed Principle (OCP)](#o---openclosed-principle-ocp)
- [L - Liskov Substitution Principle (LSP)](#l---liskov-substitution-principle-lsp)
- [I - Interface Segregation Principle (ISP)](#i---interface-segregation-principle-isp)
- [D - Dependency Inversion Principle (DIP)](#d---dependency-inversion-principle-dip)

## SOLID Principles Overview

The SOLID principles are a set of five design principles that promote modular and maintainable software design. Each principle emphasizes a different aspect of object-oriented design and aims to improve code readability, reusability, and extensibility. The principles are as follows:

- Single Responsibility Principle (SRP): A class should have only one reason to change.
- Open/Closed Principle (OCP): Software entities should be open for extension but closed for modification.
- Liskov Substitution Principle (LSP): Subtypes must be substitutable for their base types.
- Interface Segregation Principle (ISP): Clients should not be forced to depend on interfaces they do not use.
- Dependency Inversion Principle (DIP): High-level modules should not depend on low-level modules. Both should depend on abstractions.

Let's explore each principle in detail.

## S - Single Responsibility Principle (SRP)

The Single Responsibility Principle states that a class should have only one reason to change. It means that a class should have a single responsibility or a single job. This principle promotes high cohesion by ensuring that each class focuses on a specific functionality.

Example:

<details>
<summary><b>Solution</b></summary>

The code below represents a simplified example of a `Customer` class, along with a `CustomerRepository`, `CustomerService`, `EmailServices`, and `IdentificationServices` classes. Let's analyze it in terms of the Single Responsibility Principle (SRP).

The Single Responsibility Principle states that a class should have only one reason to change. It means that a class should have a single responsibility or a single job. Let's see how the principle applies to the code:


```csharp
public class Customer
{
	public int CustomerId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Identification { get; set; }
	public DateTime CreateDate { get; set; }

	public bool IsValid()
	{
		return EmailServices.IsValid(Email) && IdentificationServices.IsValid(Identification);
	}
}
```
In the `Customer` class, the properties represent the attributes of a customer, and the IsValid method checks whether the email and identification are valid using the `EmailServices` and `IdentificationServices` classes. This class is responsible for maintaining customer data and performing basic validation related to the customer object. It adheres to the SRP by focusing solely on the responsibility of encapsulating customer data.
```csharp
public class CustomerRepository
{
	public void AddCustomer(Customer customer)
	{
		using (var cn = new SqlConnection())
		{
			var cmd = new SqlCommand();

			cn.ConnectionString = "SolutionConnectionString";
			cmd.Connection = cn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "INSERT INTO CUSTOMER (NAME, EMAIL IDENTIFICATION, CREATEDATE) VALUES (@name, @email, @identification, @createDate))";

			cmd.Parameters.AddWithValue("name", customer.Name);
			cmd.Parameters.AddWithValue("email", customer.Email);
			cmd.Parameters.AddWithValue("identification", customer.Identification);
			cmd.Parameters.AddWithValue("createDate", customer.CreateDate);

			cn.Open();
			cmd.ExecuteNonQuery();
		}
	}
}
```
The `CustomerRepository` class is responsible for persisting customer data to a database. It has a single responsibility of handling database operations related to customers. It adheres to the SRP by focusing solely on the responsibility of interacting with the database.
```csharp
public class CustomerService
{
	public string AddCustomer(Customer customer)
	{
		if (!customer.IsValid())
			return "Invalid data.";

		var repo = new CustomerRepository();
		repo.AddCustomer(customer);

		EmailServices.Send("contact@company.com", customer.Email, "Welcome", "Congratulations, you are registered.");

		return "Customer successfully registered.";
	}
}
```
The `CustomerService` class is responsible for orchestrating the process of adding a customer. It first checks if the customer is valid, then interacts with the `CustomerRepository` to persist the customer data, and finally uses the `EmailServices` class to send a welcome email. The class adheres to the SRP by having a single responsibility of managing the overall process of adding a customer.
```csharp
public static class EmailServices
{
	public static bool IsValid(string email)
	{
		return email.Contains("@");
	}

	public static void Send(string from, string to, string subject, string message)
	{
		var mail = new MailMessage(from, to);
		var client = new SmtpClient
		{
			Port = 25,
			DeliveryMethod = SmtpDeliveryMethod.Network,
			UseDefaultCredentials = false,
			Host = "smtp.google.com"
		};

		mail.Subject = subject;
		mail.Body = message;
		client.Send(mail);
	}
}
```
The `EmailServices` class provides email-related functionalities. It has two methods, IsValid for email validation and Send for sending emails. It adheres to the SRP by having a single responsibility of handling email-related operations.
```csharp
public static class IdentificationServices
{
	public static bool IsValid(string identification)
	{
		return identification.Length == 11;
	}
}
```

The `IdentificationServices` class provides identification-related functionalities. It has a single method, IsValid, which validates an identification number. It adheres to the SRP by having a single responsibility of handling identification-related operations.
</details>

<details>
  <summary><b>Violation</b></summary>

The code below violates the Single Responsibility Principle (SRP) because the `Customer` class is responsible for multiple tasks that go beyond its primary responsibility of encapsulating customer data. Let's analyze the code to identify the violations

```cshar
public class Customer
{
	public int CustomerId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Identification { get; set; }
	public DateTime CreateDate { get; set; }

	public string AddCustomer()
	{
		if (!Email.Contains("@"))
			return "Customer with invalid email.";

		if (Identification.Length != 11)
			return "Customer with invalid identification.";


		using (var cn = new SqlConnection())
		{
			var cmd = new SqlCommand();

			cn.ConnectionString = "SolutionConnectionString";
			cmd.Connection = cn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "INSERT INTO CUSTOMER (NAME, EMAIL IDENTIFICATION, CREATEDATE) VALUES (@name, @email, @identification, @createDate))";

			cmd.Parameters.AddWithValue("name", Name);
			cmd.Parameters.AddWithValue("email", Email);
			cmd.Parameters.AddWithValue("identification", Identification);
			cmd.Parameters.AddWithValue("createDate", CreateDate);

			cn.Open();
			cmd.ExecuteNonQuery();
		}

		var mail = new MailMessage("contact@company.com", Email);
		var client = new SmtpClient
		{
			Port = 25,
			DeliveryMethod = SmtpDeliveryMethod.Network,
			UseDefaultCredentials = false,
			Host = "smtp.google.com"
		};

		mail.Subject = "Welcome";
		mail.Body = "Congratulations! You are registered.";
		client.Send(mail);

		return "Customer successfully registered!";
	}
}
```


1.**Validation logic**: The `AddCustomer method` performs email and identification validation. However, validation is not the primary responsibility of the `Customer` class. It is better to separate the validation concerns into separate classes or methods specifically dedicated to validation.

2.**Database insertion logic**: The `AddCustomer` method directly performs the task of inserting customer data into the database. This responsibility goes beyond the scope of the `Customer` class, which should focus solely on encapsulating customer data. A separate class, like a `CustomerRepository`, should handle the database operations.

3.**Email sending logic**: The `AddCustomer` method includes code for sending a welcome email to the customer. Again, this responsibility should be separated into a separate class or method dedicated to email-related operations, such as an `EmailService` class.

By combining all these responsibilities into the `Customer` class, it violates the SRP because the class has multiple reasons to change. If there are changes to the validation logic, database operations, or email sending, the `Customer` class will need to be modified, which breaks the principle of having a single responsibility.

</details>

## O - Open/Closed Principle (OCP)

The Open/Closed Principle states that software entities (classes, modules, functions) should be open for extension but closed for modification. It means that you should be able to add new functionality without changing the existing code.

Example:
<details>
<summary><b>Solution</b></summary>

The code below demonstrates an implementation of the Open-Closed Principle (OCP) to some extent. Let's analyze it in the context of the OCP:

```cshar
public abstract class DebitAccount
{
	public string TransactionNumber { get; set; }
	public abstract string Withdraw(decimal amount, string account);

	public string FormatTransaction()
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		var random = new Random();
		TransactionNumber = new string(Enumerable.Repeat(chars, 15)
		  .Select(s => s[random.Next(s.Length)]).ToArray());

		// Formatted transaction number.
		return TransactionNumber;
	} 
}
```
The `DebitAccount` class is an abstract base class that defines a common interface for different types of debit accounts. It has a TransactionNumber property and an abstract Withdraw method, which is meant to be overridden by the derived classes.

```cshar
public class DebitAccountAccount : DebitAccount
{
	public override string Withdraw(decimal amount, string account)
	{
		// Debita Conta Account
		return FormatTransaction();
	}
}
```

```cshar
public class DebitAccountInvestment : DebitAccount
{
	public override string Withdraw(decimal amount, string account)
	{
		// Debita Conta Investimento
		// Isentar Taxas
		return FormatTransaction();
	}
}
```

```cshar
public class DebitSavingsAccount : DebitAccount
{
	public override string Withdraw(decimal amount, string account)
	{
		// Validate Account Birthday
		// Debit Account Account
		return FormatTransaction();
	}
}
```

The code includes three concrete classes derived from the `DebitAccount` base class: `DebitAccountAccount`, `DebitAccountInvestment`, and `DebitSavingsAccount`. Each derived class implements the `Withdraw` method specific to its account type, overriding the abstract method defined in the base class.

The Open-Closed Principle suggests that classes should be open for extension but closed for modification. In this case, the `DebitAccount` class is closed for modification because it is an abstract class that defines a common interface. New types of debit accounts can be introduced by extending this class without modifying the existing code.

The derived classes, such as `DebitAccountAccount`, `DebitAccountInvestment`, and `DebitSavingsAccount`, represent different types of debit accounts and are open for extension. If you need to introduce a new type of debit account, you can create a new derived class that inherits from `DebitAccount` and implements the `Withdraw` method specific to that account type.
</details>

<details>
  <summary><b>Violation</b></summary>

The code below violates the Open-Closed Principle (OCP) because it does not follow the guideline of being open for extension but closed for modification. Let's analyze the code to identify the violations:

```cshar
public class DebitAccount
{
	public void Debit(decimal value, string account, TypeAccount typeAccount)
	{
		if (typeAccount == TypeAccount.Account)
		{
			// Debit Account Account
		}

		if (typeAccount == TypeAccount.SavingsAccount)
		{
			// Validate Account Birthday
			// Debit Savings Account
		}
	}
}
public enum TypeAccount
{
	Account,
	SavingsAccount
}
```


In this code, the `DebitAccount` class has a `Debit` method that takes a `value`, `account`, and `typeAccount` as parameters. The method contains conditional statements to determine the type of the account and performs different operations based on the account type.

The violations of the Open-Closed Principle are as follows:

1.**Modification required for adding new account types**: If a new account type, such as a `CreditAccount`, needs to be added, the `Debit` method would have to be modified to include a new conditional statement. This violates the principle as it requires modifying the existing code instead of simply extending it.

2.**Lack of abstraction**: The code does not utilize inheritance or abstraction to provide a common interface for different account types. Instead, it uses conditional statements to handle different account types within a single method. This makes the code less flexible and harder to extend.
</details>

## L - Liskov Substitution Principle (LSP)

The Liskov Substitution Principle states that subtypes must be substitutable for their base types. It means that objects of a superclass should be replaceable with objects of its subclasses without affecting the correctness of the program.

Example:
<details>
<summary><b>Solution</b></summary>

The code below demonstrates the Liskov Substitution Principle (LSP) to some extent. Let's analyze it in the context of the LSP:

```csharp
public abstract class Parallelogram
{
	protected Parallelogram(int height, int width)
	{
		Height = height;
		Width = width;
	}

	public double Height { get; private set; }
	public double Width { get; private set ; }
	public double Area { get { return Height * Width; } } 
}
```
```csharp
public class Retangulo : Parallelogram
{
	public Retangulo(int altura, int largura)
		:base(altura,largura)
	{

	}
}
```
```csharp
public class Square : Parallelogram
{
	public Square(int height, int width)
		: base(height, width)
	{
		if(width != height)
			throw new ArgumentException("The two sides of the square must be equal.");
	}
}
```

The code includes an abstract base class `Parallelogram` that represents a generic `Parallelogram` shape. It has properties for `Height`, `Width`, and a calculated property for `Area`. It serves as a common interface for different types of `Parallelogram`s.

There are two concrete classes derived from `Parallelogram`:

`Rectangle`: Represents a `Rectangle`. It inherits from `Parallelogram` and provides a constructor that takes `Height` and `Width` as parameters. The `Rectangle` class adheres to the Liskov Substitution Principle since it can be used in place of its base class (`Parallelogram`) without breaking the program's correctness.

`Square`: Represents a `Square`. It also inherits from `Parallelogram` but has a specialized constructor that validates whether the `Height` and `Width` are equal. If not, it throws an exception. This constraint ensures that a `Square` object must have equal sides. Although the `Square` class adds a validation constraint, it still adheres to the Liskov Substitution Principle because it maintains the behavior of the base class (`Parallelogram`) and can be used interchangeably without violating the program's correctness.

The Liskov Substitution Principle states that objects of a superclass should be replaceable with objects of its subclasses without affecting the correctness of the program. In this case, both `Rectangle` and `Square` can be used interchangeably as `Parallelogram` objects, allowing for polymorphic behavior without introducing errors.

</details>
<details>
  <summary><b>Violation</b></summary>

The code below violates the Liskov Substitution Principle (LSP). Let's analyze the code to identify the violation:

```csharp
public class AreaCalculation
{
	private static void GetAreaRectangle(Rectangle ret)
	{
		Console.Clear();
		Console.WriteLine("Rectangle area calculation");
		Console.WriteLine();
		Console.WriteLine(ret.Height + " * " + ret.Width);
		Console.WriteLine();
		Console.WriteLine(ret.Area);
		Console.ReadKey();
	}

	public static void Calculate()
	{
		var quad = new Square()
		{
			Height = 10,
			Width = 5
		};

		GetAreaRectangle(quad);
	}
}
public class Rectangle
{
	public virtual double Height { get; set; }
	public virtual double Width { get; set; }
	public double Area { get { return Height * Width; } }
}
public class Square : Rectangle
{
	public override double Height
	{
		set { base.Height = base.Width = value; }
	}

	public override double Width
	{
		set { base.Height = base.Width = value; }
	}
}
```

The violation of the Liskov Substitution Principle occurs in the `AreaCalculation ` class. The `GetArea`Rectangle`` method expects a `Rectangle` object as its parameter. However, in the `Calculate` method, an instance of `Square` is passed to `GetArea`Rectangle``. Although `Square` is a subclass of `Rectangle`, the LSP violation happens because the behavior of `Square` is not a true substitute for the behavior of `Rectangle`.

In this specific example, the violation occurs because the `Square` class overrides the `Height` and `Width` properties of the base `Rectangle` class to enforce equal sides. However, the `GetArea`Rectangle`` method is written assuming that the `Height` and `Width` properties of the `Rectangle` object will act independently.

By passing a `Square` object to a method that expects a `Rectangle`, the method may produce incorrect results or behave unexpectedly because the assumptions made about the properties of `Rectangle` objects are not valid for `Square` objects.

To resolve this violation, the design should either ensure that the `Square` class does not inherit from `Rectangle` or modify the `GetArea`Rectangle`` method and other code that relies on the `Rectangle` class to handle the behavior of `Square`s separately. By doing so, the program can correctly handle both `Rectangle`s and `Square`s without violating the Liskov Substitution Principle.
</details>


## I - Interface Segregation Principle (ISP)

The Interface Segregation Principle states that clients should not be forced to depend on interfaces they do not use. It means that you should design fine-grained interfaces tailored to the specific needs of the clients.

Example:

<details>
<summary><b>Solution</b></summary>

The code below demonstrates the Interface Segregation Principle (ISP). Let's analyze it in the context of the ISP:
```csharp
public interface ICustomerRegistration
{
	void ValidateData();
	void SaveBank();
	void SendEmail();
}
public interface IProductRegistration
{
	void ValidateData();
	void SaveBank();
}
public class CustomerRegistration : ICustomerRegistration
{
	public void ValidateData()
	{
		// Validate Identification, Email
	}

	public void SaveBank()
	{
		// Insert into the Customer table
	}

	public void SendEmail()
	{
		// Send email to customer
	}
}
public class ProductRegistration : IProductRegistration
{
	public void ValidateData()
	{
		// validate value
	}

	public void SaveBank()
	{
		// Insert Product table

	}
}
```

The code demonstrates the application of the Interface Segregation Principle by segregating interfaces into smaller, more focused units.

The ``ICustomerRegistration` `interface represents operations related to customer registration, including data validation, saving data to the bank, and sending emails to customers.

The `IProductRegistration` interface represents operations related to product registration, including data validation and saving data to the bank.

By separating the interfaces based on their specific responsibilities, the code adheres to the Interface Segregation Principle. Clients that depend on these interfaces can now implement and utilize only the methods they require, without being forced to implement unnecessary methods.

The `CustomerRegistration` class implements the ``ICustomerRegistration` `interface and provides implementations for all its methods: `ValidateData`, `SaveBank`, and SendEmail. It focuses on customer-related operations.

The ProductRegistration class implements the `IProductRegistration` interface and provides implementations for `ValidateData` and `SaveBank`. It focuses on product-related operations.

This approach allows clients to depend only on the relevant interfaces and implement the required methods, promoting code modularity, reusability, and reducing coupling between components. Clients that need to work with customer registration can depend on `ICustomerRegistration`, while clients that work with product registration can depend on `IProductRegistration`.

In summary, the code follows the Interface Segregation Principle by dividing the interfaces into smaller, more cohesive units, enabling clients to implement and use only the methods they require.

</details>
<details>
  <summary><b>Violation</b></summary>

The code below violates the Interface Segregation Principle (ISP). Let's analyze it to understand the violation:

```csharp
public interface IRegistration
{
	void ValidateData();
	void SaveBank();
	void SendEmail();
}
public class CustomerRegistration : IRegistration
{
	public void ValidateData()
	{
		// Validate Identification, Email
	}

	public void SaveBank()
	{
		// Insert into the Customer table
	}

	public void SendEmail()
	{
		// Send email to customer
	}
}
public class ProductRegistration : IRegistration
{
	public void ValidateData()
	{
		// validate value

	}

	public void SaveBank()
	{
		// Insert Product table

	}

	public void SendEmail()
	{
		// Product does not have email, what do I do now???
		throw new NotImplementedException("This method is useless.");
	}
}
```

The violation of the Interface Segregation Principle occurs in the `ProductRegistration` class. The interface `IRegistration` includes the method ``SendEmail``(), which is not relevant or applicable to the `ProductRegistration` class. The ``SendEmail`` method is specific to the customer registration process and does not make sense in the context of product registration.

By forcing the `ProductRegistration` class to implement the ``SendEmail`` method, the code violates the ISP because the class is forced to depend on a method that is not applicable or meaningful in its context. This violates the principle of segregating interfaces into smaller and more focused units that clients can implement based on their specific needs.

To resolve this violation, you should split the `IRegistration` interface into smaller, more cohesive interfaces based on the specific responsibilities and requirements of each domain. For example, you could have an `ICustomerRegistration` interface that includes the `ValidateData`, `SaveBank`, and ``SendEmail`` methods, and a separate interface for product-related operations.
</details>

## D - Dependency Inversion Principle (DIP)

The Dependency Inversion Principle states that high-level modules should not depend on low-level modules. Both should depend on abstractions. It means that high-level modules should not be tightly coupled to specific implementations but should rely on abstractions.

Example:

<details>
<summary><b>Solution</b></summary>

The code below demonstrates the Dependency Inversion Principle (DIP) by applying dependency injection and relying on abstractions rather than concrete implementations. Let's analyze it to understand the principle:

```csharp
public interface ICustomerRepository
{
	void AddCustomer(Customer customer);
}
public interface ICustomerServices
{
	string AddCustomer(Customer customer);
}
public interface IEmailServices
{
	bool IsValid(string email);
	void Send(string from, string to, string subject, string message);
}
public interface IIdentificationServices
{
	bool IsValid(string identification);
}
public class Customer
{
	private readonly IIdentificationServices _identificationServices;
	private readonly IEmailServices _emailServices;

	public Customer(
		IIdentificationServices identificationServices, 
		IEmailServices emailServices)
	{
		_identificationServices = identificationServices;
		_emailServices = emailServices;
	}

	public int CustomerId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Identification { get; set; }
	public DateTime CreateDate { get; set; }

	public bool IsValid()
	{
		return _emailServices.IsValid(Email) && _identificationServices.IsValid(Identification);
	}
}
public class CustomerRepositor : ICustomerRepository
{
	public void AddCustomer(Customer customer)
	{

		using (var cn = new SqlConnection())
		{
			var cmd = new SqlCommand();

			cn.ConnectionString = "SolutionConnectionString";
			cmd.Connection = cn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "INSERT INTO CUSTOMER (NAME, EMAIL IDENTIFICATION, CREATEDATE) VALUES (@name, @email, @identification, @createDate))";

			cmd.Parameters.AddWithValue("name", customer.Name);
			cmd.Parameters.AddWithValue("email", customer.Email);
			cmd.Parameters.AddWithValue("identification", customer.Identification);
			cmd.Parameters.AddWithValue("createDate", customer.CreateDate);

			cn.Open();
			cmd.ExecuteNonQuery();
		}

	}
}
public class CustomerServices : ICustomerServices
{
	private readonly ICustomerRepository _customerRepository;
	private readonly IEmailServices _emailServices;

	public CustomerServices(
		IEmailServices emailServices, 
		ICustomerRepository CustomerRepository)
	{
		_emailServices = emailServices;
		_customerRepository = CustomerRepository;
	}

	public string AddCustomer(Customer customer)
	{
		if (!customer.IsValid())
			return "Invalid data";

		_customerRepository.AddCustomer(customer);

		_emailServices.Send("contact@company.com", customer.Email, "Welcome", "Congratulations, you are registered");

		return "Customer successfully registered.";
	}
}
public class EmailServices : IEmailServices
{
	public bool IsValid(string email)
	{
		return email.Contains("@");
	}

	public void Send(string from, string to, string subject, string message)
	{
		var mail = new MailMessage(from, to);
		var client = new SmtpClient
		{
			Port = 25,
			DeliveryMethod = SmtpDeliveryMethod.Network,
			UseDefaultCredentials = false,
			Host = "smtp.google.com"
		};

		mail.Subject = subject;
		mail.Body = message;
		client.Send(mail);
	}
}
public class IdentificationServices : IIdentificationServices
{
	public bool IsValid(string identification)
	{
		return identification.Length == 11;
	}
}
```

The code follows the Dependency Inversion Principle by inverting the dependencies between high-level and low-level modules and relying on abstractions. Here's how it is achieved:

The `Customer` class depends on abstractions (`IIdentificationServices` and `IEmailServices`) rather than concrete implementations. This allows the class to be decoupled from specific implementations and promotes flexibility and extensibility.

The `CustomerServices` class depends on abstractions (`IEmailServices` and `ICustomerRepository`) rather than concrete implementations. By depending on interfaces, the class is not tightly coupled to specific implementations, making it easier to substitute or extend functionalities.

The `CustomerRepository` class implements the `ICustomerRepository` interface, providing a concrete implementation for adding a customer to the database. The `CustomerServices` class depends on this interface, allowing it to work with any class that satisfies the contract defined by the interface.

The `EmailServices` and `IdentificationServices` classes implement the `IEmailServices` and IIdentificationServices interfaces, respectively, providing concrete implementations for email-related operations and identification validation. The `Customer` class depends on these interfaces, enabling it to use the provided functionalities without being tightly coupled to specific implementations.

</details>
<details>
  <summary><b>Violation</b></summary>


```csharp
public class Customer
{
	public int CustomerId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Identification { get; set; }
	public DateTime CreateDate { get; set; }

	public bool IsValid()
	{
		return EmailServices.IsValid(Email) && IdentificationServices.IsValid(Identification);
	}
}
public class CustomerRepositor
{

	public void AddCustomer(Customer customer)
	{
		using (var cn = new SqlConnection())
		{
			var cmd = new SqlCommand();

			cn.ConnectionString = "SolutionConnectionString";
			cmd.Connection = cn;
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "INSERT INTO CUSTOMER (NAME, EMAIL IDENTIFICATION, CREATEDATE) VALUES (@name, @email, @identification, @createDate))";

			cmd.Parameters.AddWithValue("name", customer.Name);
			cmd.Parameters.AddWithValue("email", customer.Email);
			cmd.Parameters.AddWithValue("identification", customer.Identification);
			cmd.Parameters.AddWithValue("createDate", customer.CreateDate);

			cn.Open();
			cmd.ExecuteNonQuery();
		}
	}
}
public class CustomerServices
{
	public string AddCustomer(Customer customer)
	{
		if (!customer.IsValid())
			return "Invalid data";

		var repo = new CustomerRepositor();
		repo.AddCustomer(customer);

		EmailServices.Send("contact@company.com", customer.Email, "Welcome", "Congratulations, you are registered");
		return "Customer successfully registered.";
	}
}
public static class EmailServices
{
	public static bool IsValid(string email)
	{
		return email.Contains("@");
	}

	public static void Send(string from, string to, string subject, string message)
	{
		var mail = new MailMessage(from, to);
		var client = new SmtpClient
		{
			Port = 25,
			DeliveryMethod = SmtpDeliveryMethod.Network,
			UseDefaultCredentials = false,
			Host = "smtp.google.com"
		};

		mail.Subject = subject;
		mail.Body = message;
		client.Send(mail);
	}
}
public static class IdentificationServices
{
	public static bool IsValid(string identification)
	{
		return identification.Length == 11;
	}
}
```

The provided code violates the Dependency Inversion Principle in several ways:

1.The `Customer` class directly depends on the concrete implementation of `EmailServices` and `IdentificationServices` static classes. Instead of depending on abstractions (interfaces), it tightly couples itself to specific implementations. This makes it difficult to substitute or extend the functionality of these services without modifying the `Customer` class.

2.The `CustomerRepository` class is directly instantiated within the `CustomerServices` class. This violates the principle as it creates a tight coupling between the `CustomerServices` and `CustomerRepository` classes. It should depend on abstractions (interfaces) rather than concrete implementations to allow for flexibility and easy replacement of the repository implementation.

3.The `EmailServices` and `IdentificationServices` classes are implemented as static classes, which makes them difficult to substitute or mock during testing. Static dependencies limit the ability to provide alternative implementations and introduce dependencies that cannot be easily changed.

To adhere to the Dependency Inversion Principle, the code should be refactored to:

1.Introduce interfaces for the services (IEmailServices and IIdentificationServices) and have the `Customer` class depend on these interfaces instead of the concrete implementations.

2.Modify the `CustomerServices` class to accept the ICustomerRepository interface and IEmailServices interface as constructor parameters. This enables dependency injection and decouples the `CustomerServices` class from specific implementations.

3.Implement the `CustomerRepository` class to satisfy the ICustomerRepository interface. This allows for easy substitution of repository implementations without affecting the `CustomerServices` class.

</details>


## Conclusion

The SOLID principles offer valuable guidelines for designing maintainable and flexible software systems. By applying these principles, you can improve code quality, modularity, and extensibility. This repository serves as a practical demonstration of these principles in action.

Feel free to explore the code examples provided in this repository to gain a better understanding of how to apply the SOLID principles in your own projects.


---

**Contact Information**

**Name:** João Marcelo de Mello

**Email:** [joaomarcelog@gmail.com](mailto:joaomarcelog@gmail.com)

---

git


