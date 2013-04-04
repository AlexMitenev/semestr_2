//Alex Mitenev 2012 

let mulPifogorNumber n =
  let mutable res = 0.0
  let mutable a = 1.0
  let mutable b = 1.0
  while a < n - 1.0 do
    b <- 1.0
    while b < n - 1.0 do
      let c : float = sqrt (float(a*a + b*b)) 
      if a + b + c = n then  
        res <- c * a * b
        printfn "%A" c
        printfn "%A" a
        printfn "%A" b
      b <- b + 1.0
    a <- a + 1.0
  res

printf "%A" <| mulPifogorNumber 12.0
