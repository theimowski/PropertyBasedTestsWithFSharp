module Demo
open Xunit
open FsCheck
open FsCheck.Xunit

let add x y = x + y
    
//[<Property>]
let ``add is commutative``(x, y) =
    add x y = add y x

//[<Property>]
let ``0 is the identity element of add`` (x) =
    add x 0 = x

//[<Property>]
let ``add is associative`` (x, y, z) =
    add x (add y z) = add (add x y) z