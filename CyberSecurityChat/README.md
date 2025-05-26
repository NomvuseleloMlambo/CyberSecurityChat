GitHub link: https://github.com/NomvuseleloMlambo/CyberSecurityChat.git 

# CyberSecurityChat Bot 

# Project Overview

CyberSecurityChat is a console-based C# chatbot designed to raise awareness about online safety and best practices in cybersecurity. The bot interacts with the user, answers cybersecurity-related questions, and offers emotional support when users express concern, curiosity, or frustration.

It's ideal for educational environments, beginner chatbot developers, or as a base for more advanced conversational bots.

# Features

-  Responds to cybersecurity topics like **phishing**, **malware**, **passwords**, **safe browsing**, and more.
-  Provides **multiple random phishing tips** to keep responses fresh.
-  Detects user **sentiment** (concern, curiosity, frustration) and replies with supportive messages.
-  Recognizes general phrases like "how are you" or "what can I ask".
-  Remembers the last topic for repeat questions like “can you repeat that?”
-  Displays a welcome **ASCII banner** and plays a **greeting sound**.

#Technologies Used

- Language: C#
- Framework: .NET Console Application
- Additional: `System.Media` for audio, `Thread.Sleep` for typing effect

# How to Run the Project

1. Open the solution in Visual Studio or your preferred C# IDE.
2. Add a **greeting.wav** sound file in a `Resources` folder within the project directory.
3. Build and run the application.
4. Interact with the bot via the terminal.

# Sample Test Run


What's your name?
> Alex

Welcome, Alex! I'm your Cybersecurity Awareness Assistant.
Ask me anything about staying safe online!

What would you like to ask about cybersecurity? (Type 'exit' to quit)
> Tell me about phishing

Be cautious of emails asking for personal information.

> another tip

Here's another phishing tip: Scammers often disguise themselves as trusted organizations.

> I'm frustrated this is confusing

I get that this can be frustrating. Let’s take it one step at a time — I’m here to make it easier.

> What did you say about phishing?

Sure! Here's what I said about phishing:
Scammers often disguise themselves as trusted organizations.

> What can I ask?

You can ask about:
- Phishing
- Strong passwords
- Malware
- Safe browsing
- Common scams
- Social engineering

> exit

Goodbye, Alex! Stay safe online. 
