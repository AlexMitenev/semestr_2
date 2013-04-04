open System

let isPal (n : int) =
  let string = Convert.ToString n
  let mutable firstPos = 0
  let mutable endPos = Core.String.length string - 1
  let mutable sumCountedChar = 0
  while endPos > firstPos && string.[firstPos] = string.[endPos] do
    endPos <- endPos - 1
    firstPos <- firstPos + 1  
    sumCountedChar <- sumCountedChar + 2
  if sumCountedChar + 1 >= String.length string 
    then true
    else false

let maxPal numDigit =
  let mutable maxP = 0
  let min = int(10.0 ** (numDigit - 1.0)) 
  let max = int(10.0 ** numDigit - 1.0)
  let mutable num1 = max
  let mutable num2 = max
  while num1 >= min do
    num2 <- max
    while num2 >= min do
      let mul = num1 * num2
      if isPal mul = true  && mul > maxP then 
        maxP <- mul
        printfn "%d" num1
        printfn "%d" num2
      num2 <- num2 - 1
    num1 <- num1 - 1
  maxP    
    
printf "%A" <| maxPal 3.0
  
  
