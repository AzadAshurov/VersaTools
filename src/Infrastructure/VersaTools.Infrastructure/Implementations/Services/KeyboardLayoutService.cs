using System.Text;
using VersaTools.Application.Abstractions.Services;

namespace VersaTools.Infrastructure.Implementations.Services
{
    public class KeyboardLayoutService : IKeyboardLayoutService
    {
        private readonly Dictionary<char, char> _toCyrillic = new()
        {
            ['q'] = 'й',
            ['w'] = 'ц',
            ['e'] = 'у',
            ['r'] = 'к',
            ['t'] = 'е',
            ['y'] = 'н',
            ['u'] = 'г',
            ['i'] = 'ш',
            ['o'] = 'щ',
            ['p'] = 'з',
            ['a'] = 'ф',
            ['s'] = 'ы',
            ['d'] = 'в',
            ['f'] = 'а',
            ['g'] = 'п',
            ['h'] = 'р',
            ['j'] = 'о',
            ['k'] = 'л',
            ['l'] = 'д',
            ['z'] = 'я',
            ['x'] = 'ч',
            ['c'] = 'с',
            ['v'] = 'м',
            ['b'] = 'и',
            ['n'] = 'т',
            ['m'] = 'ь',
            ['`'] = 'ё',
            ['['] = 'х',
            [']'] = 'ъ',
            [';'] = 'ж',
            ['\''] = 'э',
            [','] = 'б',
            ['.'] = 'ю',
            ['/'] = '.'
        };

        private readonly Dictionary<char, char> _toLatin = new();

        public KeyboardLayoutService()
        {
            foreach (var pair in _toCyrillic)
            {
                _toLatin[pair.Value] = pair.Key;
            }
        }

        public string ConvertToCyrillic(string input)
        {
            StringBuilder result = new();
            foreach (char c in input)
            {
                result.Append(_toCyrillic.TryGetValue(char.ToLower(c), out char converted)
                    ? (char.IsUpper(c) ? char.ToUpper(converted) : converted)
                    : c);
            }
            return result.ToString();
        }

        public string ConvertToLatin(string input)
        {
            StringBuilder result = new();
            foreach (char c in input)
            {
                result.Append(_toLatin.TryGetValue(char.ToLower(c), out char converted)
                    ? (char.IsUpper(c) ? char.ToUpper(converted) : converted)
                    : c);
            }
            return result.ToString();
        }
    }
}
