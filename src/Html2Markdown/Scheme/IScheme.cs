using System.Collections.Generic;
using Html2Markdown.Replacement;

namespace Html2Markdown.Scheme {
	public interface IScheme {
		IList<IReplacer> Replacers();
	}
}