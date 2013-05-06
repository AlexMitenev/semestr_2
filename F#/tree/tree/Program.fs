//Alex Mitenev 2012

type Tree =
  | Empty
  | Node of int * Tree * Tree

let rec addElement tree n =
  match tree with
    | Empty -> Node(n, Empty, Empty)
    | Node(value, left, right) when n < value -> Node(value, addElement left n, right)
    | Node(value, left, right) when n > value -> Node(value, left, addElement right n)

let rec find tree n =
  match tree with
    | Empty -> false
    | Node(value, left, right) when n < value -> find left n
    | Node(value, left, right) when n > value -> find right n
    | Node(value, left, right) when n = value -> true

let rec findMin tree = 
    match tree with
      | Node(value, Empty, right) -> value
      | Node(value, left, right) -> findMin left


let rec deleteElement tree n =
  match tree with
    | Empty -> Empty
    | Node(value, left, right) when n < value -> Node(value, deleteElement left n, right)
    | Node(value, left, right) when n > value -> Node(value, left, deleteElement right n)

    | Node(value, Empty, Empty) when n = value -> Empty
    | Node(value, Empty, Node(valNext, leftNext, rightNext)) when n = value -> Node(valNext, leftNext, rightNext)
    | Node(value, Node(valNext, leftNext, rightNext), Empty) when n = value -> Node(valNext, leftNext, rightNext)
    | Node(value, left,  right) when n = value -> Node(findMin right, left, deleteElement right <|findMin right)

let rec printSpases num =
  match num with
    |0 -> printf "%s" ""
    |n when n > 0 -> (printf "%c" ' ')
                     printSpases (n - 1)

let rec print tree offset = 
  printSpases offset
  match tree with
    | Empty -> printfn "%s" "Nill"
    | Node(value, left, right) -> printfn "%d" value
                                  print left (offset + 2)
                                  print right (offset + 2)


//example
let myTree = Empty
let myTree1 = addElement myTree 5
let myTree2 = addElement myTree1 2
let myTree3 = addElement myTree2 8
let myTree4 = addElement myTree3 4
let myTree5 = addElement myTree4 1
let myTree6 = addElement myTree5 9
let myTree7 = addElement myTree6 0
//let myTree8 = deleteElement myTree7 5
print myTree7 0

(*let t =
 Empty
 |> addElement 5
 |> addElement 2!!!!!!!!!!!!!!!!!*)
