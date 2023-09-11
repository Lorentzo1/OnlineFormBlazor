# OnlineFormBlazor
## This is a work in progress project that simulates a hypothetical "HR" application.
The application allows to create new profiles(person), with personal details and assign roles to those profiles. Later, the profiles can be edited or deleted in an overview of all the profiles created.

## Technologies used
The Solution consists of two projects. 

### Blazor WASM called BlazorOnlineForm 
It represents the UI for the user to work with. It allows to navigate to different pages. Use **Form** page where a new Form(person) can be added. The page utilizes data annotation validation (including costume ones),
combo box select, multi select, validation summery, and popups.
**Form List** pages allows to view all created persons, and manage them. You can either delete of edit a person.
The app is capable to react to back-end errors. Some are not critical, allowing to progress, some errors will escalate into Error popup informing the user to try the operation later.
No 3th party UI libraries were used for any of the components.

### WebApi called OnlineFormApi
This Rest-Api supplies a back end for the Blazor WASM app. It maps the Dto to an entity model. It works with DB(using EF core) allowing for basic CRUD operations on the Person model. It supplies data like, list of countries and
roles(many to many) allowed to be assigned. It also creates a json file with every Person created and tracks the edited Perons files. The Api informs the Blazor app of any errors with DB operations.

## Run the app
In order to run the app. Insert your connection string to appsettings.json into the property DefaultConnectionString. And apply migrations with the 'Update-Databse' command in package manager. 
