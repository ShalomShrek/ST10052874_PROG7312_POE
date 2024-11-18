# ST10052874_PROG7312_POE
ST10052874
Setup:
To download the application, please follow the link https://github.com/ShalomShrek/ST10052874_PROG7312_POE/tree/master to the GitHub repo. From there click the green code button, and select either open in Visual Studio or download Zip. Using either option open the program in Visual Studio, and run the application by pressing the green triangle button. This will start the application.
Part 1:
Upon running the application, the main menu will be loaded. This will show 3 buttons to navigate to the report issues page, local events and announcements page, and Service Request Status page. Please note for Part 1 that the buttons for the local events and announcements page, and Service Request Status page, are disabled. There is also the option to navigate to the user survey page (the selected user engagement feature) via the nav bar at the top of the screen.

Upon clicking the report issues page button, the report issues page will be loaded. The user will be required to add the details about the issue they are reporting, including the location, type of issue, description of the issue, and any media (such as photos or videos) to provide more context of the issue. The user will be prompted to complete all fields if any fields are not filled in before attempting to submit it. Upon successfully submitting a report, the issue is added to a list of all reports that have been made. This list is also displayed below the submit report button, to show that the reports are being stored successfully. Note that the list will be lost upon the programs termination, as the POE only requires the storage of data in a list for part 1, not a database. The user will be able to navigate to the main menu via a button at the bottom of the page.

User engagement feature: User feedback Surveys
Upon clicking the user survey button, the user will be taken to the survey page. If the user wishes to comment on the application, such as potential feature suggestions, bug reporting, or improvements to existing features, they will be able to make these known by submitting this information via the survey page. These surveys will also be stored in lists, and will also be lost upon the applications termination. The idea of the feature is to make the user feel their voices and opinions are being considered in the design of the application, thus resulting in a more personal connection to the application, resulting in the user using the application more. The list of surveys will also be displayed to show the data is being stored. The user can navigate back to the main menu via a button at the bottom of the screen.

Part 2:

If the user selects Local Events and announcements, the user with be navigated to the local events page.

The user will be shown a list of all the events in the area. If the user wishes to filter the events based on various options, such as name or date, the user can select to filter those options.

The application will also take into account the users searches, and using machine learning, will make recommendations based on the users previous searches.

This part of the application makes use of queues to store and fetch the data, and dictionaries to track search patterns and hash sets to help searching for categories.

The data in this section is pre-loaded.

Part 3:

If the user selects Service request status, the user will be taken to the status page. This will display the current status of their previous requests. 

The program loads 3 pre-loaded requests for display purposes, but any requests added via part 1's feature will also be displayed.

The user can filter based on the status of the requests, or by the location.

Binary search trees are used for the display of the data, while Heaps are used for the filtering of the data.
