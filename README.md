# Switter - A Social Networking Application
## Overview
**Switter** is a social networking app inspired by Twitter, allowing users to post, view, and manage messages called "sweets." It includes both a Server module and a Client module.

## Features
### Core Features
- **User Connections:** Clients connect to the server using a GUI and a unique username.
- **Posting Sweets:** Users can post messages, which the server stores with the username, unique ID, and timestamp.
- **Viewing Sweets:** Users can request and view sweets from other users.
### Enhanced User Interaction
- **User List:** Clients can request and see a list of all users.
- **Following Users:** Users can follow others to receive their updates.
- **Sweet Feeds:** Users can request a feed of sweets from all users or just from those they follow.
### Advanced User Management
- **Blocking Users:** Users can block others to prevent them from following or viewing their sweets.
- **Followers and Following Lists:** Users can see who follows them and who they are following.
- **Deleting Sweets:** Users can delete their own sweets using the unique sweet ID.
## Server Module
- **Listening for Connections:** The server listens on a specified port for incoming connections.
- **Handling Multiple Clients:** Supports multiple clients at once, ensuring unique usernames.
- **Activity Logging:** Logs all activities like connections, posts, follows, and errors in the Server GUI.
- **Persistent Storage:** Stores sweets and user relationships persistently.
## Client Module
- **Connecting to Server:** Clients enter the server’s IP and port through the GUI to connect.
- **User Interface:** GUI for posting sweets, following/blocking users, viewing feeds, and user lists.
- **Activity Logging:** Logs all client activities in the Client GUI.
- **TCP Sockets:** Uses TCP sockets for reliable communication with the server.
### Technical Details
- **Languages:** C#.
- **TCP Sockets:** Ensures reliable communication between client and server.
- **GUI:** Both client and server have graphical interfaces.
- **Error Handling:** Ensures smooth operation and clean shutdowns.
### Conclusion
Switter is a user-friendly social networking application with features for posting, following, and managing sweets, leveraging TCP sockets and graphical interfaces for a seamless experience.
