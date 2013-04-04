//Alex Mitenev 2013

let sum n =
  let mutable s = 0
  for i = 1 to n-1 do
    if i % 3 = 0 || i % 5 = 0 then
      s <- s + i
  s
printfn "%d" <|sum 1000
