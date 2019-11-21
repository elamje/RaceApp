Setup:

* Repository
  * `git clone https://github.com/elamje/RaceApp.git`

* appsettings.json
  * I'm using a docker container to host SQL Server on my Mac, so please change your connection string to what you normally use!
  * I'm using gmail for smtp relay: If you want to use that, you need to login to your google account -> security -> enable less secure apps, If you have two factor auth enabled, there is another step I believe. 
  * There are a few migrations to run (dotnet ef database update Enduro2EventSeedData should get you up to date)



Application Decisions

* Using Data Annotation Validation Attributes local to the Model.
  * For a larger project, validation might be done better in a separate service, with custom logic rather than local to the model.
* Admin section to create events is out of scope.
* API not necessary.
* Using Strings for Car Information.
  * Not sure how rigid F1 Cars are, i.e. using a predefined list of options for Make, Model, Engine, etc. or allowing User to enter info.
* Didn't add entity metadata like createdOn, updatedOn timestamps since there wasn't much to sort.
* Did not use the Repository pattern for size of project.
* The application is not internationalized, although F1 is quite international.
* Using Identity Package adds a lot of unused columns, these can be stripped out in this case.
* Relaxed Password Requirements for this app for development/running locally.
* Workaround in CarController for Editing and maintaining a FK during the context.
  * Code Scaffolding caused a headache here after I committed to the model. Caused issue for my User foreign key.
* I didn't check many queries to make sure they returned a value.
* I combined registration and event logic into the Event Controller, but for larger project would break these apart.
* Used ViewModels in the Controller rather than a separate directory.
* Log emails sent in DB.
* Use a worker queue to handle emails/long running processes.
* Try to understand how to access Users more efficiently in .NET Core 3
  * I tried to leverage Identity a little too hard, and I believe I could have made some middleware to keep track of users a little cleaner.
* Add fields to viewmodels for filtering views.
* Since there is no "checkout" process, only a simple email message about discount qualification - I did not inform the user that they no longer qualify for a discount if they deregister from an event that previously qualified them.
* Not sure if Car Numbers must be unique, so I left the validation out.
***
Learned:
* The .NET Core APIs change a lot.
* Code scaffolding works sometimes, but is more of a headache other times.
* There are many ways to do Models & ViewModels which I can see will cause maintainability issues if all of your models & viewmodels are a slightly different pattern.
* I could really use Visual Studio and SSMS!
***
Next Time:
* Use one style of model/viewmodel pattern for simplicity.
  * Code Generator made a couple of Razor Page style templates which were weird to switch between.
***
Data Model (->> Relationship)

* User
  * First, Last, Email, Pass
  * Car->>
  * Registration->>
* Event
  * Type[Short = 2, Enduro = 1], Cost, Discounted Cost, Date, Nullable WeekendCountSinceEpoch, Name, Description
  * Registration->>
  * meta: Event Dates might not be on weekends, so need to leave Weekend of Year nullable
* Car
  * Car Num, Make, Model, Nullable Engine type, Nullable Engine builder name
  * User->>
  * Registrations->>
* Registration
  * DiscountQualified
  * User->>
  * Car->>
  * Event->>

Navigation/Control Buttons

* Logout
* Account
  * Register & Deregister here
* Events
* Cars
