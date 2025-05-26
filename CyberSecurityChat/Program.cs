using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace CyberSecurityChat
{
    class Program
    {
        // Tracks the most recent topic the user asked about
        static string lastTopic = "";

        // Stores history of topic responses
        static Dictionary<string, string> topicHistory = new Dictionary<string, string>();

        // Stores static one-line responses for certain cybersecurity topics
        static Dictionary<string, string> staticTopicResponses = new Dictionary<string, string>
        {
            { "password", "\nMake sure to use strong, unique passwords for each account. Avoid using personal info like your birthday." },
            { "malware", "\nMalware is malicious software like viruses, spyware, or ransomware that can damage or steal your data." },
            { "safe browsing", "\nSafe browsing means avoiding unknown websites, not clicking suspicious links, and using secure connections." },
            { "social engineering", "\nSocial engineering is when someone tricks you into giving away confidential information by pretending to be someone you trust." },
            { "scam", "\nWatch out for online scams. Never click on suspicious links or give out personal information to unverified sources." },
            { "privacy", "\nProtect your privacy by adjusting security settings on social media and apps. Only share personal information when absolutely necessary." }
        };

        // Stores topics with multiple tips, randomly selected when asked
        static Dictionary<string, List<string>> randomTopicResponses = new Dictionary<string, List<string>>
        {
            { "phishing", new List<string>
                {
                    "Be cautious of emails asking for personal information.",
                    "Scammers often disguise themselves as trusted organizations.",
                    "Never click on suspicious links in emails or messages.",
                    "Always verify the sender's email address.",
                    "Look for grammar mistakes in suspicious emails."
                }
            }
        };

        // Entry point of the application
        static void Main(string[] args)
        {
            PlayGreeting();      // Play startup audio
            ShowAsciiArt();      // Show ASCII art banner
            Thread.Sleep(11000); // Pause for 11 seconds
            string userName = GreetUser(); // Ask for user name
            StartChatbot(userName);        // Begin chatbot interaction
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();             // Wait before closing
        }
        // Plays a greeting sound if available
        static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Resources/greeting.wav");
                player.Load();
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Audio error: " + ex.Message);
            }
        }
        // Displays the CyberBot ASCII logo
        static void ShowAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"     
                     ________
                    / _______\ 
                   | |      | |  
                   | | LOCK | |   
                   | |______| | 
                    \________/    

                  [CYBER BOT]
               Cybersecurity Awareness
");
            Console.ResetColor();
        }
        // Greets the user and asks for their name
        static string GreetUser()
        {
            Console.WriteLine("\nWhat's your name?");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid name.");
                Console.ResetColor();
                name = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome, {name}! I'm your Cybersecurity Awareness Assistant.");
            Console.WriteLine("Ask me anything about staying safe online!");
            Console.ResetColor();

            return name;
        }

        // Main chatbot loop: takes user input, processes, and gives responses
        static void StartChatbot(string userName)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeResponse("\nWhat would you like to ask about cybersecurity? (Type 'exit' to quit)");
                Console.ResetColor();

                string userInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    TypeResponse("\nI didn’t catch that. Please type your question again.");
                    continue;
                }

                if (userInput.ToLower() == "exit")
                {
                    Console.WriteLine($"\nGoodbye, {userName}! Stay safe online.");
                    break;
                }

                GetResponse(userInput);
            }
        }

        // Analyzes user input and gives the appropriate chatbot response
        static void GetResponse(string userInput)
        {
            userInput = userInput.ToLower();

            string sentiment = DetectSentiment(userInput);
            HandleSentiment(sentiment); // Show a calming message if needed

            if (CheckRepeatRequest(userInput)) return;
            if (HandleGeneralQueries(userInput)) return;
            if (HandleRandomTopic(userInput)) return;

            // Look through static responses
            foreach (var topic in staticTopicResponses.Keys)
            {
                if (userInput.Contains(topic))
                {
                    lastTopic = topic;
                    string response = staticTopicResponses[topic];
                    TypeResponse(response);
                    topicHistory[topic] = response;
                    return;
                }
            }

            // If nothing matched, show a fallback message
            Console.ForegroundColor = ConsoleColor.Yellow;
            TypeResponse("\nI'm not sure I understand. Can you try rephrasing or ask about a cybersecurity topic like phishing or malware?");
            Console.ResetColor();
        }

        // Detects the emotional tone of the user’s input
        static string DetectSentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous") || input.Contains("anxious") || input.Contains("concern"))
                return "concern";
            else if (input.Contains("curious") || input.Contains("interested") || input.Contains("wondering"))
                return "curiosity";
            else if (input.Contains("frustrated") || input.Contains("annoyed") || input.Contains("confused") || input.Contains("overwhelmed"))
                return "frustration";
            else
                return "neutral";
        }

        // Displays a supportive message based on detected sentiment
        static void HandleSentiment(string sentiment)
        {
            switch (sentiment)
            {
                case "concern":
                    TypeResponse("\nIt's completely understandable to feel that way. Cyber threats can be stressful, but I'm here to help you feel more secure.");
                    break;
                case "curiosity":
                    TypeResponse("\nCuriosity is great! The more you learn about cybersecurity, the better prepared you'll be.");
                    break;
                case "frustration":
                    TypeResponse("\nI get that this can be frustrating. Let’s take it one step at a time — I’m here to make it easier.");
                    break;
            }
        }

        // Checks if user wants the bot to repeat a previous topic
        static bool CheckRepeatRequest(string input)
        {
            if (input.Contains("remind") || input.Contains("what did you say") || input.Contains("repeat") || input.Contains("again"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && topicHistory.ContainsKey(lastTopic))
                {
                    TypeResponse($"\nSure! Here's what I said about {lastTopic}:");
                    TypeResponse(topicHistory[lastTopic]);
                }
                else
                {
                    TypeResponse("\nI can't recall a topic yet. Try asking me something first.");
                }
                return true;
            }
            return false;
        }

        // Handles general chatbot questions like "how are you?" or "what can I ask?"
        static bool HandleGeneralQueries(string input)
        {
            if (input.Contains("how are you"))
            {
                TypeResponse("\nI'm doing great, thank you! I'm always ready to help you stay safe online.");
                return true;
            }
            else if (input.Contains("what's your purpose") || input.Contains("what is your purpose"))
            {
                TypeResponse("\nMy purpose is to educate and assist you with cybersecurity knowledge so you can protect yourself online.");
                return true;
            }
            else if (input.Contains("what can i ask") || input.Contains("topics"))
            {
                TypeResponse("\nYou can ask about:\n- Phishing\n- Strong passwords\n- Malware\n- Safe browsing\n- Common scams\n- Social engineering");
                return true;
            }
            return false;
        }

        // Handles responses for topics like "phishing" that have multiple tips
        static bool HandleRandomTopic(string input)
        {
            if (lastTopic == "phishing" && (input.Contains("more") || input.Contains("another tip")))
            {
                string tip = GetRandomResponse("phishing");
                TypeResponse("\nHere's another phishing tip: " + tip);
                topicHistory["phishing"] = tip;
                return true;
            }
            else if (input.Contains("phishing"))
            {
                lastTopic = "phishing";
                string tip = GetRandomResponse("phishing");
                TypeResponse("\n" + tip);
                topicHistory["phishing"] = tip;
                return true;
            }
            return false;
        }

        // Selects a random message from a list for a given topic
        static string GetRandomResponse(string topic)
        {
            Random rand = new Random();
            List<string> responses = randomTopicResponses[topic];
            return responses[rand.Next(responses.Count)];
        }

        // Simulates typing the chatbot's response
        static void TypeResponse(string message, int delay = 25)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}
