# Limit Number of Sessions per set of Pages

Let's assume the application was designed to work on two set of devices:

- A "scada-like" set of pages that are populated with a big number of elements
- A "mobile-friendly" set of pages with minimal content that allows getting a system overview

As the number of clients of the WebPresentationEngine grows, the system could be overloaded if too many clients are requesting the "scada-like" set of pages, while many more clients can connect to the mobile version without negatively impacting on the system.

This project allows monitoring the number of connected clients requesting each set of pages, and forbids access to those pages if exceeding the session count limits set in `Optix_Sample_LimitNumberOfSessionsPerPagesSet/UI/Screens/WelcomeScreen/SessionsMonitoring`.

## Testing the project

To test this project, please open a set of browser tabs to `http://127.0.0.1:8080` and select the main or sub set of pages.

## Disclaimer

Rockwell Automation maintains these repositories as a convenience to you and other users. Although Rockwell Automation reserves the right at any time and for any reason to refuse access to edit or remove content from this Repository, you acknowledge and agree to accept sole responsibility and liability for any Repository content posted, transmitted, downloaded, or used by you. Rockwell Automation has no obligation to monitor or update Repository content

The examples provided are to be used as a reference for building your own application and should not be used in production as-is. It is recommended to adapt the example for the purpose, observing the highest safety standards.
