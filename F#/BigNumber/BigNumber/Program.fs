open System
open Core.Operators

let convertListToString list =
  match list with
  |[] -> ""
  |hd::tl when hd < 0 -> "-" + List.fold (fun (acc : string) elem -> acc + string(elem * (-1))) "" list
  |hd::tl -> List.fold (fun (acc : string) elem -> acc + string(elem)) "" list

type BigNumber(s : string) =
  let Base = 10
  let StringNumber = s
  
  member public this.BigOrEqual(original : BigNumber) =

    let rec compare l1 l2 =
      match l1,l2 with
      |[],[] -> true
      |[], hd::tl when hd > 0 -> false
      |[], hd::tl -> true
      |hd::tl, [] when hd > 0 -> true 
      |hd::tl, [] -> false
      |hd1::tl1, hd2::tl2 when hd1 < hd2 -> false
      |hd1::tl1, hd2::tl2 -> compare tl1 tl2

    compare this.Value original.Value
        
  member public this.ConvertToIntList =
    let StringNumWithoutMinus = StringNumber.Trim([|'-'|])
    let CharArrNumber = StringNumWithoutMinus.ToCharArray()

    let sign c =  if c = '-' then -1 else 1
    Array.toList <| Array.map (fun x -> Int32.Parse (string x) * sign(StringNumber.[0])) CharArrNumber 
   
  member public this.ConvertToMinus =
    let conv l = List.foldBack (fun x acc -> (x*(-1)) :: acc) l []
    new BigNumber(convertListToString <| (conv this.Value))

  static member (~-) (n : BigNumber) =
    n.ConvertToMinus

  member public this.Value =
    this.ConvertToIntList

  member public this.Print =
   printfn "%s"  <| convertListToString(this.Value)

  member public this.Length =
    this.ConvertToIntList.Length

  member public this.deleteNulElem =
    let l = this.Value
    let rec delete l = 
      match l with
        | [] -> []
        | [0] -> [0]
        | hd::tl when hd <> 0 -> l
        | hd:: tl -> delete tl
    new BigNumber(convertListToString <|delete l)

  static member Balance(this : int list, compare : int list) =
    let balance (l1 : int list) (l2 : int list) =
      let differense = l2.Length - l1.Length
      [ for i in 1 .. differense -> 0 ] @l1
    balance this compare

  member public this.Plus(summand : BigNumber) =
    let thisList = BigNumber.Balance (this.Value,summand.Value)
    let summandList = BigNumber.Balance (summand.Value, this.Value)

    let sign x = 
      match x with
      |n when n < 0 -> -1
      |n when n > Base -> 1
      |n -> 0

    let modul x =
      match x with
      |n when n < 0 -> n + Base
      |n when n > Base -> n - Base
      |n -> n

    let add (l1 : int list) (l2 : int list) =
      let listSum = List.foldBack2 (fun elem1 elem2 acc ->
                                    match acc with
                                    | [] -> (sign <| elem1 + elem2) ::((modul <| elem1 + elem2) :: acc)
                                    | hd::tl -> (sign <| elem1 + elem2 + hd)::(modul <| elem1 + elem2 + hd) :: tl)                           
                                    l1
                                    l2
                                    []
      listSum
    let BigSum = new BigNumber(convertListToString <|add thisList summandList)
    if BigSum.Value <> [0] then BigSum.deleteNulElem else BigSum

  static member (+) (n1 : BigNumber, n2 : BigNumber) =
    n1.Plus(n2)

  member public this.Minus(subtrahend : BigNumber) =
    match this.BigOrEqual(subtrahend) with
    |true -> this + (-subtrahend)
    |false ->subtrahend + (-this)

  static member (-) (n1 : BigNumber, n2 : BigNumber) =
    n1.Minus(n2)

  member public this.Multyply(factor : BigNumber) =
    let Nul = new BigNumber("0")

    let rec mulOneDigit (acc : BigNumber) n =
      match n with
      |0 -> acc
      |n when n > 0 -> mulOneDigit (acc + this) (n-1)
      |n -> mulOneDigit (acc - this) (n + 1)
    
    let putNul n (big : BigNumber) =
      let value = big.Value @ [ for i in 1 .. n -> 0 ]
      new BigNumber(convertListToString value)

    let rec mul l accNum (acc : BigNumber) = 
      match l with
      | [] -> acc
      | hd::tl -> acc + (mulOneDigit Nul hd |> putNul accNum) + (mul tl (accNum - 1) Nul)
    mul factor.Value (factor.Length - 1)  Nul

  static member (*) (n1 : BigNumber, n2 : BigNumber) =
    n1.Multyply(n2)

let n1 = new BigNumber("464")
let n2 = new BigNumber("2123")
let mul = -n1 * (-n2) + n1
mul.Print
