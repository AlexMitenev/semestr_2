open System
open Core.Operators

let convertListToString list =
  List.fold (fun (acc : string) elem -> acc + string(elem)) "" list

type BigNumber(s : string) =

  //divide the line into integers of length 4
  let StringNumber = s

  member public this.ConvertToIntList =

    (*let LengthFirstElem = (StringNumber.Length) % 4
    let NumberElem = (StringNumber.Length - 1) / 4 + 1
    let Number = Array.create NumberElem ""

    let mutable k = 0
    if LengthFirstElem <> 0 then
      for i = 0 to LengthFirstElem - 1 do
        Number.[0] <- Number.[0] + string(StringNumber.[i]) 
      k <- 1
    let NewCharNumber = StringNumber.Remove (0, LengthFirstElem)
    for i = k to NumberElem - 1 do
      for j = 1 to 4 do
        Number.[i] <- Number.[i] + string(NewCharNumber.[(i - k) * 4 + j - 1])*)
    
    //Array.toList <| Array.map (Int32.Parse) Number

    let CharArrNumber = StringNumber.ToCharArray()
    let IntArrNumber = Array.map (fun x -> Int32.Parse (string x)) CharArrNumber  
    Array.toList IntArrNumber 
  
  member public this.Value =
    this.ConvertToIntList

  member public this.Print =
   printf "%s"  <| convertListToString(this.ConvertToIntList)

  member public this.Length =
    this.ConvertToIntList.Length

  member public this.deleteNulElem =
    let l = this.ConvertToIntList
    let rec delete l =    
      match l with
        | [] -> l
        | hd::tl when hd <> 0 -> l
        | hd:: tl when hd = 0 -> delete tl
    let goodNumber = new BigNumber(convertListToString <|delete l)
    goodNumber

type Ariphmetics =
  
  static member Balance(n1 : BigNumber, n2 : BigNumber)=
    
    let listN1 = n1.Value
    let listN2 = n2.Value


    let balance (l1 : int list) (l2 : int list) =
      let differense = l2.Length - l1.Length
      let listOfDiff = [ for i in 1 .. differense -> 0 ]
      let listAnswer = listOfDiff @ l1
      listAnswer

    let stringAnswer = convertListToString <| balance listN1 listN2
    let answer = new BigNumber(stringAnswer)
    answer
    
  

  static member Plus(n1 : BigNumber, n2 : BigNumber) =

    let listN1 =  Ariphmetics.Balance(n1, n2).Value
    let listN2 =  Ariphmetics.Balance(n2, n1).Value

    let add (l1 : int list) (l2 : int list) =
      let listSum = List.foldBack2 (fun elem1 elem2 acc ->
                                    match acc with
                                    | [] -> ((elem1 + elem2) / 10) ::(((elem1 + elem2) % 10) :: acc)
                                    | hd::tl ->  ((elem1 + elem2 + hd) / 10)::((elem1 + elem2 + hd) % 10) :: tl)                                                             
                                    l1
                                    l2
                                    []
      listSum

    let stringNum = convertListToString <|add listN1 listN2
    let BigSum = new BigNumber(stringNum)
    BigSum.deleteNulElem



  static member Minus(n1 : BigNumber, n2 : BigNumber) =

    let listN1 =  Ariphmetics.Balance(n1, n2).Value
    let listN2 =  Ariphmetics.Balance(n2, n1).Value
    
    let sub (l1 : int list) (l2 : int list) =
      let listSub = List.foldBack2 (fun elem1 elem2 acc ->
                                    match acc with
                                    | [] -> (sign(elem1 - elem2)) ::(modul(elem1 - elem2) :: acc)
                                    | hd::tl ->  (sign(elem1 - elem2 + hd))::(modul(elem1 - elem2 + hd)) :: tl)                                                             
                                    l1
                                    l2
                                    []
      listSub
    let stringNum = convertListToString <|sub listN1 listN2
    let BigSub = new BigNumber(stringNum)
    BigSub.deleteNulElem

let BigN1 = new BigNumber("1000057560")
let BigN2 = new BigNumber("100")
let sum = Ariphmetics.Plus (BigN1, BigN2)
let sub = Ariphmetics.Minus (BigN1, BigN2)
sub.Print




                                  