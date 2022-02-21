namespace src;
public class FST
{
    private readonly IDictionary<char, FST> _nodes;
    private readonly char _value;

    public FST(char value)
    {
        _value = value;
        _nodes = new Dictionary<char, FST>();
    }

    private bool IsFinal() 
    {
        return _nodes.Count() == 0;
    }
    private FST InsertChild(char c, ref Queue<char> input) 
    {
        if (!_nodes.ContainsKey(c)) {
            _nodes.Add(c, new FST(c));
        }
        return _nodes[c];
    }

    public void Insert(string word)
    {
        Queue<char> q = new(word.ToCharArray());
        var next = InsertChild(q.Dequeue(), ref q);
        while (q.Count > 0) {
            next = next.InsertChild(q.Dequeue(), ref q);
        }
    }

    private FST Match(char c, out bool match) {
        if (!_nodes.ContainsKey(c)) {
            match = false;
            return new FST(' ');
        }

        match = true;
        return _nodes[c];
    }

    private bool ReachFinalState(FST fst, ref Queue<char> chars) 
    {
        if (fst.IsFinal()) return true;

        if (!(fst._nodes.Count == 1)) {
            return false;
        }
        
        var self = fst._nodes.First().Value;
        while (!self.IsFinal()) {
            if (self._nodes.Count == 1) {
                chars.Enqueue(self._value);
                self = self._nodes.First().Value;
            } else {
                return false;
            }
        }

        chars.Enqueue(self._value);

        return true;
    }

    public bool Complete(string partialWord, out string completedWord) 
    {
        Queue<char> q = new(partialWord.ToCharArray());
        var next = Match(q.Dequeue(), out bool match);
        
        if (!match) 
        {
            completedWord = "";
            return false;
        }

        while (q.Count > 0) {
            next = next.Match(q.Dequeue(), out bool innerMatch);
            if (!innerMatch) {
                completedWord = "";
                return false;
            }
        }

        completedWord = partialWord;
        Queue<char> restOfWord = new();
        var canReachFinalState = ReachFinalState(next, ref restOfWord);

        if (canReachFinalState) 
        {
            completedWord += string.Join("", restOfWord);
            return true;
        }

        return false;
    }
}