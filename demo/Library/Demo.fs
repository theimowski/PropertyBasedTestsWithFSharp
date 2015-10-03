module Demo

let add x y = 
    x + y

open Xunit

[<Fact>]
let ``5 add 3 gives 8``() =
    let actual = add 5 3
    Assert.Equal(8, actual)

type Amount = decimal
type Currency = USD | EUR
type Money = Currency * Amount
type Salary = Yearly of Money

type Customer =
    { Age : int 
      Salary : Salary }

[<Fact>]
let ``rich customer without liabilities ``() =
    // Arrange phase
    let customer = 
        { Age = 55
          Salary = Yearly (USD, 150000.M) }
    ()

type CustomerClassification = 
    { IsImportantCustomer : bool }

let classifyCustomer _ = { IsImportantCustomer = true }

[<Fact>]
let ``aa``() =
    // Arrange phase
    let inputXml = 
        """<customer>
                <firstName>John</firstName>
                <lastName>Doe</lastName>
                <email>john.doe@example.com</com>
                <subscriptions>
                    <subscription>
                        <type>...
                ...
           </customer>
        """
    // Act phase
    let result = classifyCustomer inputXml

    // Assert phase
    Assert.True(result.IsImportantCustomer)