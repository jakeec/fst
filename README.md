# Finite State Transducer (FST)

This is a very naive implementation of an FST used to autocomplete words. It works by storing each character of a word as a node in a tree structure.

```
            ()
           /  \
          c    d
         /    / \
        a    o   i
       / \   |   |
      t   r  g   g
       \   \ |  /
        ((     ))        
```

When you provide an incomplete word it traverses the tree up to the point where there are no more input characters to consume and then checks to see if it can reach a final state deterministically (i.e. only one child on each successive node). If the last node you land on after consuming all the input could go down more than one branch at any point before reaching a final state it won't find a match. 

I might update this to return a list of possible matches and then the final node from the input string will see if it can reach a final state via all of its children and return the ones that can. 

There is currently a failing test using "Agamem" as the input and expecting "Agamemnon" but the actual result being "Agamemnon's". This highlights a bug where words that are contained within the start of another word get overwritten by the longer word. This is because there are no actual final state nodes, instead a final node will simply have no children. In order to fix this I'll have to create final state nodes with some terminating character so that any given node can have both a connection to a final state and have children that lead to longer words. The method to autocomplete given a parial word will just return whichever complete word terminates first.

This FST does not currently include any sort of edit distance mechanism so only inputs that are exact subsets of words in the set will find matches. 