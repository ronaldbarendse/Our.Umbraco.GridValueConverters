# Our.Umbraco.GridValueConverters
**Provides a property value converter that creates a strongly typed model of the Umbraco grid and its controls/editors.**

The base package (`Our.Umbraco.GridValueConverters`) contains:
* The strongly typed models (interfaces and classes) for the grid, sections, rows, areas, controls and editors;
* The property value converter for the Umbraco grid (`GridValueConverter`);
* JsonConverters that use a resolver to convert control and editor interfaces to specific types when deserializing (`GridControlConverter` for `Grid.IControl` and `GridEditorConverter` for `Grid.IEditor`);
* A resolver that resolves control and editor types based on the editor properties (`GridControlsResolver`);
* A `GridControlAttribute` that can be applied to control classes and is used by default to specify for which grid editor alias or view the control must be resolved;
* Two methods for setting the resolver: `ApplicationEventHandler` and `GridWebBootManager` (the latter could be used when integrating to Umbraco core);
* A base class for controls with HTML-encoded values (`HtmlGridControl`);
* HTML helpers for rendering conditional HTML tags, grid tags and grid controls.

The default package (`Our.Umbraco.GridValueConverters.Default`) contains:
* Models for default Umbraco grid controls (embed, macro, media, rte and textstring);
* Partial views for rendering the strongly typed model (compatible with `@Html.GetGridHtml()`).

### Install
This package is available as [NuGet](https://www.nuget.org/packages/Our.Umbraco.GridValueConverters/) and [Umbraco](https://our.umbraco.org/projects/developer-tools/ourumbracogridvalueconverters/) package. The recommended install method is NuGet:

`PM> Install-Package Our.Umbraco.GridValueConverters`

`PM> Install-Package Our.Umbraco.GridValueConverters.Default`

### Explanation
The Umbraco grid property editor stores it's value/content in the database as JSON. The default (built-in) property value converter merges the saved JSON with the grid editor configuration (so it's always current) and returns a `Newtonsoft.Json.Linq.JObject`.

After installing this package, the default property value converter is replaced/extended to return a `Our.Umbraco.GridValueConverters.Models.Grid`. Nothing more, nothing less: now you're ready to use the strongly typed model - with autocomplete/IntelliSense - in your views... It's just like magic!

Well, actually not real magic:
* The JSON object is converted/deserialized to the strongly typed model using [Newtonsoft.Json](https://www.newtonsoft.com/json);
* Grid controls and editors are interfaces, so JsonConverters are used to convert these to specific types using a resolver;
* The resolver is set up to load all `Grid.IControl` types on application start and call `IsControlEditor()`, untill it finds the specific type for the requested grid editor;
* The default `Grid.Control` type has an implementation of the `IsControlEditor()` method that checks for the `GridControlAttribute` attribute and calls the `IsControlEditor()` method of the attribute. This makes it possible to change this implementation in 2 different ways:
  * Implement your own `Grid.IControl`;
  * Inherit from the attribute (but keep in mind: this only works if the control where the attribute is applied inherits from `Grid.Control`).

#### HTML Helpers
The base package includes HTML helpers to make rendering the grid a little easier.

To render rows and areas with optional settings (configuration/attributes and styles), to following helper can be used:

```
@using (Html.BeginGridTag(Grid.ISettings gridSettings, string tagName = "div", object htmlAttributes = null))
{
}
```

Using the same syntax, conditional tags can be rendered (if the condition is false, only the opening and closing tags will be omitted):

```
@using (Html.BeginTag(string tagName, bool condition = true, object htmlAttributes = null))
{
}
```

Grid controls are basically just partial views in a predefined folder (`Views\Grid\Partials\Editors`). The partial view name, however, can be retrieved/inferred from the editors `render`, `view` or `alias` properties (if not explicitly defined). The equivalents of `@Html.Partial()` and `Html.RenderPartial()` are: 

```
@Html.GridControl(Grid.IControl gridControl, string partialViewName = null)
Html.RenderGridControl(Grid.IControl gridControl, string partialViewName = null)
```

### Default grid controls
The default package includes views for rendering the 4 default frameworks (Bootstrap 2, Bootstrap 2 fuild, Bootstrap 3 and Bootstrap 3 fluid) and the following controls/editors:
* embed (`EmbedGridControl`): embed HTML code;
* macro (`MacroGridControl`): the value contains the macro alias and parameters;
* media (`MediaGridControl`): the value contains the image details (id, image URL, focal point, alternate text and caption) and the editor config the predefined image size. Also has methods to easily get the crop URL: `GetCropUrl()` and `GetCrop()`;
* rte (`RteGridControl`): HTML code with parsed internal links and resolved URLs;
* textstring (`TextstringGridControl`): HTML encoded value with applied markup and styles from the editor config.

### Roadmap
* Add tests;
* Benchmark conversions/lookups and cache resulting control and editor types by grid alias (if needed);
* Proxy control to dynamic object (for full backwards compatibility);
* Add example Examine indexer;
* Integrate into Umbraco core!