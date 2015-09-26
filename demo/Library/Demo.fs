module Demo

let add x y = 
    x + y

open Xunit

[<Fact>]
let ``5 add 3 gives 8``() =
    let actual = add 5 3
    Assert.Equal(8, actual)