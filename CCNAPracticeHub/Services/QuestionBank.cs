
using CCNAPracticeHub.Models;

namespace CCNAPracticeHub.Services;

public static class QuestionBank
{
    public static List<Question> GetQuestions()
    {
        return new List<Question>
        {
            new Question
            {
                Id = 1,
                Domain = "Network Fundamentals",
                Subtopic = "Network Devices",
                Difficulty = "Easy",
                QuestionText =
                    "Which network device forwards packets between different IP networks?",
                Options = new()
                {
                    "Layer 2 switch",
                    "Router",
                    "Wireless access point",
                    "Repeater"
                },
                CorrectAnswerIndex = 1,
                Explanation =
                    "A router operates at Layer 3 of the OSI model and forwards packets between IP networks using routing information."
            },

            new Question
            {
                Id = 2,
                Domain = "Network Fundamentals",
                Subtopic = "IPv4 Addressing & Subnetting",
                Difficulty = "Easy",
                QuestionText =
                    "Which subnet mask corresponds to a /24 IPv4 prefix?",
                Options = new()
                {
                    "255.255.0.0",
                    "255.255.255.128",
                    "255.255.255.0",
                    "255.255.255.192"
                },
                CorrectAnswerIndex = 2,
                Explanation =
                    "A /24 prefix has 24 network bits, represented by the subnet mask 255.255.255.0."
            },

            new Question
            {
                Id = 3,
                Domain = "Network Fundamentals",
                Subtopic = "IPv6 Addressing",
                Difficulty = "Easy",
                QuestionText =
                    "How many bits are in an IPv6 address?",
                Options = new()
                {
                    "32 bits",
                    "64 bits",
                    "128 bits",
                    "256 bits"
                },
                CorrectAnswerIndex = 2,
                Explanation =
                    "IPv6 uses 128-bit addresses, compared with the 32-bit addresses used by IPv4."
            },

            new Question
            {
                Id = 4,
                Domain = "Network Fundamentals",
                Subtopic = "TCP & UDP",
                Difficulty = "Medium",
                QuestionText =
                    "Which transport protocol establishes a connection using a three-way handshake?",
                Options = new()
                {
                    "UDP",
                    "TCP",
                    "ICMP",
                    "IP"
                },
                CorrectAnswerIndex = 1,
                Explanation =
                    "TCP uses a three-way handshake involving SYN, SYN-ACK and ACK to establish a connection."
            },

            new Question
            {
                Id = 5,
                Domain = "Network Access",
                Subtopic = "VLANs",
                Difficulty = "Easy",
                QuestionText =
                    "What is the primary purpose of creating VLANs on a switch?",
                Options = new()
                {
                    "Increase Ethernet cable speed",
                    "Eliminate the need for IP addresses",
                    "Create separate logical broadcast domains",
                    "Automatically encrypt network traffic"
                },
                CorrectAnswerIndex = 2,
                Explanation =
                    "VLANs divide a switched network into separate logical broadcast domains, helping segment traffic."
            },

            new Question
            {
                Id = 6,
                Domain = "IP Connectivity",
                Subtopic = "Static Routing",
                Difficulty = "Medium",
                QuestionText =
                    "Which Cisco IOS command configures a default IPv4 static route through next-hop 192.168.1.1?",
                Options = new()
                {
                    "ip route 0.0.0.0 0.0.0.0 192.168.1.1",
                    "ip route 255.255.255.255 0.0.0.0 192.168.1.1",
                    "ip default-gateway 0.0.0.0 192.168.1.1",
                    "ip route 192.168.1.1 255.255.255.255 0.0.0.0"
                },
                CorrectAnswerIndex = 0,
                Explanation =
                    "The command ip route 0.0.0.0 0.0.0.0 192.168.1.1 creates a default static route with the specified next-hop address."
            },

            new Question
            {
                Id = 7,
                Domain = "IP Services",
                Subtopic = "DHCP",
                Difficulty = "Easy",
                QuestionText =
                    "Which service automatically provides IPv4 configuration information to clients?",
                Options = new()
                {
                    "DNS",
                    "NTP",
                    "SNMP",
                    "DHCP"
                },
                CorrectAnswerIndex = 3,
                Explanation =
                    "DHCP can automatically provide clients with IPv4 addresses, subnet masks, default gateways and DNS server information."
            },

            new Question
            {
                Id = 8,
                Domain = "IP Services",
                Subtopic = "DNS",
                Difficulty = "Easy",
                QuestionText =
                    "Which DNS record type maps a hostname to an IPv4 address?",
                Options = new()
                {
                    "MX",
                    "AAAA",
                    "A",
                    "CNAME"
                },
                CorrectAnswerIndex = 2,
                Explanation =
                    "An A record associates a hostname with an IPv4 address. An AAAA record is used for IPv6 addresses."
            },

            new Question
            {
                Id = 9,
                Domain = "Security Fundamentals",
                Subtopic = "Access Control Lists",
                Difficulty = "Medium",
                QuestionText =
                    "Which type of Cisco IPv4 ACL can filter traffic using source and destination IP addresses and TCP or UDP port numbers?",
                Options = new()
                {
                    "Standard ACL",
                    "Extended ACL",
                    "MAC address table",
                    "VLAN database"
                },
                CorrectAnswerIndex = 1,
                Explanation =
                    "Extended IPv4 ACLs can filter traffic using source and destination addresses, protocols and relevant port numbers."
            },

            new Question
            {
                Id = 10,
                Domain = "Automation and Programmability",
                Subtopic = "JSON",
                Difficulty = "Easy",
                QuestionText =
                    "Which format commonly represents structured data using objects containing key-value pairs?",
                Options = new()
                {
                    "JSON",
                    "ARP",
                    "ICMP",
                    "STP"
                },
                CorrectAnswerIndex = 0,
                Explanation =
                    "JSON represents structured data using objects, arrays and values. Objects contain key-value pairs."
            }
        };
    }
}
