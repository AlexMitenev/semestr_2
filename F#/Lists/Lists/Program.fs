//Alex Mitenev 2013

let addToEnd l n = 
  let l = n :: l
  let rec add l =
    match l with
      | [ _ ] -> l
      | hd::tl -> (List.head tl) :: (add(hd :: List.tail tl))
  add l   

let rec append l1 l2 = 
  match l2 with
    | [] -> l1
    | hd::tl -> append (addToEnd l1 hd) tl
  
let reverse l =
  let l2 = []
  let rec rev l l2 = 
    match l with
      |[] -> l2
      |hd::tl ->  rev tl (hd::l2)
  rev l l2

let isEven n = 
  if n % 2 = 0 then true else false

let rec find l f =
    match l with
    | hd::tl when f hd = true -> Some (hd)
    | hd::tl when f hd = false -> find tl f
    | [] -> None

let map l f =
  let makeList acc x =
    addToEnd acc <| f x
  let NewList = List.fold (makeList) [] l
  NewList
 
//examples
printfn "%A" <| addToEnd [1;2;3;4;5] 6
printfn "%A" <| append [1;2;3;4;5] [1;2;3]
printfn "%A" <| reverse [1;2;3]
printfn "%A" <| find [1;2;3;4;5;6] isEven
printfn "%A" <| map [1;2;3] isEven
