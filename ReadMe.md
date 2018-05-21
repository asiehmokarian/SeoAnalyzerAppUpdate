# SEO Analyzer Application
## Description
This web application is a simplified SEO analyzer that performs a simple SEO analysis of the text. User submits a text in English or URL, page filters out stop-words (e.g. ‘or’, ‘and’, ‘a’, ‘the’ etc), calculates number of occurrences of each word on the page, number of occurrences of each word listed in meta keywords tag on the page and the number of external links in the text.
## Application Inputs
- User can submit a Text or URL.
- Analysis options: 
    - Calculates the number of occurrences of each word on the page
    - Calculates the number of occurrences of each word listed in meta keywords tag on the page.
    - Calculates the number of external links

The options can be turned on or off but at least one option should be chosen.

### Application Outputs
The analysis results will be shown in sortable tables.

### Assumptions
- The submitted text can be a plain text or a text that contains HTML tags.
- Application filters out stop-words and they are configurable via `Web.config` file.
- The pattern for detecting each word is configurable via `Web.config` file.
- Absolute URIs are considered as external links and relative links are considered as internal links.

### Implementation
The application is implemented as an ASP .NET MVC application with Visual Studio 2017 using .Net framework 4.6.1, JQuery 3.3.1, Bootstrap 3.3.7.

### Run the Application
Simply open the project and press F5.

### User Manual
- Choose the type of input which can be text or url.
- Text/URL is required.
- Text can be a plain text or and html text.
- The URL should be a valid URL. URLs with the format of `http://example.com/...` or `https://example.com/...` are considered valid. Submitting invalid URL or unreachable URL will make error.
- Choose analysis options, at least one option should be chosen.
- Click analyze button to run the analyzer.
- For each enabled option, the result will be shown in a sortable table.

