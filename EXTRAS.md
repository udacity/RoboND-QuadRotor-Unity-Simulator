
# Extra tools for data collection

While working on the follow me project, I found a bit troublesome to record the data one path at a time. Essentially, the approach was the following :

*   Make paths for quad and hero, and make spawn points.
*   Make a recording.
*   Clear everything and repeat for another section of the environment.

This works, but I found it difficult when trying to get large amounts of data, as I had to repeat the process again and again for each recording. If I made a mistake, then I should clear the path and start over, and if I had a working configuration, I could not save it for later editing or re-usage.

To solve these issues I made some changes to the codebase of the simulator, adding some tools solve these issues.

## **Modified approach**

The approach I took was to allow the user edition and saving/loading of the paths and spawn points he's currently editing. Now, the steps would look like this (modified state machine) :

![APPROACH SUMMARY](./imgs/img_modified_approach.png)

The functionality from the previous version is kept as usual, and we just added some extra functionality on top of the current codebase to avoid conflicts.

### _**Schedules**_

The paths for the hero and quad, as well as the spawn points for the crowd are abstracted into a single object called [**PathSampleCompound**](https://github.com/wpumacay/RoboND-QuadRotor-Unity-Simulator/blob/master/Assets/Scripts/PathPlanner.cs) (had to make this in order to make it serializable and allow saving to a json easily).

Then, this is further abstracted into an object called [**DataExtractionSchedule**](https://github.com/wpumacay/RoboND-QuadRotor-Unity-Simulator/blob/master/Assets/Scripts/DataExtraction/DataExtractionSchedule.cs), which holds a compound, as well as some more functionality for batch recording, visualization, etc.

### _**DataExtractionManager**_

This is the class that holds almost all of the functionality that I added. It holds a list of the current schedules, it has the link to the key presses to allow the user to use this functionality, and more. This is a singleton, and some code integration was needed in a few places, and I checked this integrations does not break the normal default operation (if something breaks, just let me know at wilsanph@gmail.com).

These are the features exposed to the user using the appropiate keys :

*   **Home** and **End** keys : allow the user to go to the next or previous schedule.
*   **Z** : loads the schedules (request for a folder and loads all .json files in that folder).
*   **X** : saves the current schedules as .json files into disk (request the user for a location and save each schedule as a separate .json file in that folder).
*   **U** : begins batch recording.
*   **V** : begins batch recording for only the current schedule.
*   **PageDown** : toggles the current edition mode for the schedules (in edition mode the user can change the name and mode of recording of the current schedule).
*   **Delete** : removes current schedule.
*   **Insert** : creates a schedule from the working managers (hero, path and spawn) and adds it to the schedules list.
*   **Return** : Changes the follow mode of the schedule to either **free** (patrols the path in the schedule), **follow** (follows the target in the schedule) and **follow-far** (same as previous, but at a bigger distance of the target).

### _**Batch recording**_

This is the main feature I made these changes for. This allows the user to send a single request to record data from all schedules so far. It does so one by one until all have finished recording the data (each one is hard-coded to take a batch of 1000 images, for now, but can be extracted to another option).