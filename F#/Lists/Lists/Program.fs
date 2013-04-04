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
  
let rec reverse l =
  match l with
    |[] -> l
    |hd::tl -> hd::reverse(tl)

printfn "%A" <| addToEnd [1;2;3;4;5] 6
printfn "%A" <| append [1;2;3;4;5] [1;2;3]
