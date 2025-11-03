The project follows a bespoke file and structure, based upon the functionality of the files in each folder.

- In Utilities we find all files that help the app run as it should, such as helper methods, menu drivers, global/appwide information and the logo
These are generally used to drive the program and hold the data, but is rarely interacted with by your average user.

- In the folder Users we find the structure of different types of users, and the parent/blueprint for all Users, BasicUser
Functionality and data from these files serve as the main interaction for the user of the app, and Users are also saved in a Json file which is created locally.
More user types may be added, but must in such case inherit from the BasicUser class, and modify the Json settings to also account for this new derived class

- Accounts hold data for how to construct an account, a loan, and a transfer. These are relatively small classes that could likely rather easily be modified or added to.
Since accounts may be saved to a Json file, please keep in mind that these settings must be updated for any new derived types from account.

- UI contains functionality for separate menus, any new menus (not menu drivers! They go in Utilities) created should go in this folder.
