﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace commonItems {
    public class GameVersion : Parser {
        private int? firstPart;
        private int? secondPart;
        private int? thirdPart;
        private int? fourthPart;

        public GameVersion(int? theFirstPart,
            int? theSecondPart,
            int? theThirdPart,
            int? theFourthPart) {
            firstPart = theFirstPart;
            secondPart = theSecondPart;
            thirdPart = theThirdPart;
            fourthPart = theFourthPart;
        }

        public GameVersion(string version) {
            if (string.IsNullOrEmpty(version)) {
                return;
            }

            var dot = version.IndexOf('.');
            firstPart = int.Parse(version.Substring(0, dot));
            if (dot == -1) {
                return;
            }

            version = version.Substring(dot + 1);
            dot = version.IndexOf('.');
            secondPart = int.Parse(version.Substring(0, dot));
            if (dot == -1) {
                return;
            }

            version = version.Substring(dot + 1);
            dot = version.IndexOf('.');
            thirdPart = int.Parse(version.Substring(0, dot));
            if (dot == -1) {
                return;
            }

            version = version.Substring(dot + 1);
            dot = version.IndexOf('.');
            fourthPart = int.Parse(version.Substring(0, dot));
        }

        public GameVersion(BufferedReader reader) {
            RegisterKeys();
            ParseStream(reader);
            ClearRegisteredRules();
        }

        private void RegisterKeys() {
            RegisterKeyword("first", (reader) => {
                firstPart = new SingleInt(reader).Int;
            });
            RegisterKeyword("second", (reader) => {
                secondPart = new SingleInt(reader).Int;
            });
            RegisterKeyword("third", (reader) => {
                thirdPart = new SingleInt(reader).Int;
            });
            RegisterKeyword("forth", (reader) => {
                fourthPart = new SingleInt(reader).Int;
            });
            RegisterRegex(CommonRegexes.Catchall, ParserHelpers.IgnoreAndLogItem);
        }

        public override bool Equals(object? obj) {
            if (obj is not GameVersion rhs) {
                return false;
            }
            var testL = 0;
            var testR = 0;
            if (firstPart != null) {
                testL = firstPart.Value;
            }

            if (rhs.firstPart != null) {
                testR = rhs.firstPart.Value;
            }

            if (testL != testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (secondPart != null) {
                testL = secondPart.Value;
            }

            if (rhs.secondPart != null) {
                testR = rhs.secondPart.Value;
            }

            if (testL != testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (thirdPart != null) {
                testL = thirdPart.Value;
            }

            if (rhs.thirdPart != null) {
                testR = rhs.thirdPart.Value;
            }

            if (testL != testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (fourthPart != null) {
                testL = fourthPart.Value;
            }

            if (rhs.fourthPart != null) {
                testR = rhs.fourthPart.Value;
            }

            if (testL != testR) {
                return false;
            }

            return true;
        }

        public override int GetHashCode() {
            return HashCode.Combine(firstPart, secondPart, thirdPart, fourthPart);
        }

        public static bool operator >=(GameVersion lhs, GameVersion rhs) {
            return lhs > rhs || lhs.Equals(rhs);
        }
        public static bool operator >(GameVersion lhs, GameVersion rhs) {
            int testL = 0;
            int testR = 0;
            if (lhs.firstPart != null) {
                testL = lhs.firstPart.Value;
            }
            if (rhs.firstPart != null) {
                testR = rhs.firstPart.Value;
            }

            if (testL > testR) {
                return true;
            }
            if (testL < testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (lhs.secondPart != null) {
                testL = lhs.secondPart.Value;
            }

            if (rhs.secondPart != null) {
                testR = rhs.secondPart.Value;
            }

            if (testL > testR) {
                return true;
            }

            if (testL < testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (lhs.thirdPart != null) {
                testL = lhs.thirdPart.Value;
            }

            if (rhs.thirdPart != null) {
                testR = rhs.thirdPart.Value;
            }

            if (testL > testR) {
                return true;
            }

            if (testL < testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (lhs.fourthPart != null) {
                testL = lhs.fourthPart.Value;
            }

            if (rhs.fourthPart != null) {
                testR = rhs.fourthPart.Value;
            }

            if (testL > testR) {
                return true;
            }

            return false;
        }

        public static bool operator <(GameVersion lhs, GameVersion rhs) {
            var testL = 0;
            var testR = 0;
            if (lhs.firstPart != null) {
                testL = lhs.firstPart.Value;
            }

            if (rhs.firstPart != null) {
                testR = rhs.firstPart.Value;
            }

            if (testL < testR) {
                return true;
            }

            if (testL > testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (lhs.secondPart != null) {
                testL = lhs.secondPart.Value;
            }

            if (rhs.secondPart != null) {
                testR = rhs.secondPart.Value;
            }

            if (testL < testR) {
                return true;
            }

            if (testL > testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (lhs.thirdPart != null) {
                testL = lhs.thirdPart.Value;
            }

            if (rhs.thirdPart != null) {
                testR = rhs.thirdPart.Value;
            }

            if (testL < testR) {
                return true;
            }

            if (testL > testR) {
                return false;
            }

            testL = 0;
            testR = 0;
            if (lhs.fourthPart != null) {
                testL = lhs.fourthPart.Value;
            }

            if (rhs.fourthPart != null) {
                testR = rhs.fourthPart.Value;
            }

            if (testL < testR) {
                return true;
            }

            return false;
        }

        public static bool operator <=(GameVersion lhs, GameVersion rhs) {
            return lhs < rhs || lhs.Equals(rhs);
        }

        public override string ToString() {
            var sb = new StringBuilder();
            if (firstPart != null) {
                sb.Append(firstPart.Value);
                sb.Append('.');
            } else {
                sb.Append("0.");
            }
            if (secondPart != null) {
                sb.Append(secondPart.Value);
                sb.Append('.');
            } else {
                sb.Append("0.");
            }
            if (thirdPart != null) {
                sb.Append(thirdPart.Value);
                sb.Append('.');
            } else {
                sb.Append("0.");
            }
            if (fourthPart != null) {
                sb.Append(fourthPart.Value);
                sb.Append('.');
            } else {
                sb.Append("0.");
            }
            return sb.ToString();
        }

        public string ToShortString() {
            var sb = new StringBuilder();
            if (fourthPart != null) {
                sb.Append('.');
                sb.Append(fourthPart.Value);
            }
            if (thirdPart != null) {
                sb.Insert(0, thirdPart.Value);
                sb.Insert(0, '.');
            }
            if (secondPart != null) {
                sb.Insert(0, secondPart.Value);
                sb.Insert(0, '.');
            }
            if (firstPart != null) {
                sb.Insert(0, firstPart.Value);
                sb.Insert(0, '.');
            }
            return sb.ToString();
        }

        public string ToWildCard() {
            var sb = new StringBuilder();
            if (fourthPart != null) {
                sb.Append('.');
                sb.Append(fourthPart.Value);
            } else if (thirdPart != null) {
                sb.Append(".*");
            }

            if (thirdPart != null) {
                sb.Insert(0, thirdPart.Value);
                sb.Insert(0, '.');
            } else if (secondPart != null) {
                sb.Clear();
                sb.Append(".*");
            }

            if (secondPart != null) {
                sb.Insert(0, secondPart.Value);
                sb.Insert(0, '.');
            } else if (firstPart != null) {
                sb.Clear();
                sb.Append(".*");
            }

            if (firstPart != null) {
                sb.Insert(0, firstPart.Value);
            } else {
                sb.Clear();
                sb.Append('*');
            }

            return sb.ToString();
        }

        // Largerish is intended for fuzzy comparisons like "converter works with up to 1.9",
        // so everything incoming on rhs from 0.0.0.0 to 1.9.x.y will match, (where x and y are >= 0),
        // thus overshooting the internal "1.9.0.0" setup. This works if ".0.0" are actually undefined.
        public bool IsLargerishThan(GameVersion rhs) {
            var testDigit = 0;
            if (rhs.firstPart != null) {
                testDigit = rhs.firstPart.Value;
            }

            if (firstPart != null && testDigit > firstPart) {
                return false;
            }

            testDigit = 0;
            if (rhs.secondPart != null) {
                testDigit = rhs.secondPart.Value;
            }

            if (secondPart != null && testDigit > secondPart) {
                return false;
            }

            testDigit = 0;
            if (rhs.thirdPart != null) {
                testDigit = rhs.thirdPart.Value;
            }

            if (thirdPart != null && testDigit > thirdPart) {
                return false;
            }

            testDigit = 0;
            if (rhs.fourthPart != null) {
                testDigit = rhs.fourthPart.Value;
            }

            if (fourthPart != null && testDigit > fourthPart) {
                return false;
            }

            return true;
        }

        GameVersion? ExtractVersionFromLauncher(string filePath) {
            // use this for modern PDX games, point filePath to launcher-settings.json to get installation version.

            if (!File.Exists(filePath)) {
                Logger.Log(LogLevel.Warning, "Failure extracting version: " + filePath + " does not exist.");
                return null;
            }

            var result = ExtractVersionByStringFromLauncher("rawVersion", filePath);
            if (result == null) {
                // imperator rome?
                result = ExtractVersionByStringFromLauncher("version", filePath);
            }
            if (result == null) {
                Logger.Log(LogLevel.Warning, "Failure extracting version: " + filePath + " does not contain installation version!");
                return null;
            }
            return result;
        }

        GameVersion? ExtractVersionByStringFromLauncher(string versionString, string filePath)
        {
            try {
                using (StreamReader sr = File.OpenText(filePath)) {
                    while (!sr.EndOfStream) {
                        string? line = sr.ReadLine();
                        if (line == null || !line.Contains(versionString, StringComparison.InvariantCulture)) {
                            continue;
                        }
                        var pos = line.IndexOf(':');
                        if (pos == -1) {
                            sr.Close();
                            return null;
                        }

                        line = line.Substring(pos + 1);
                        pos = line.IndexOf('\"');
                        if (pos == -1) {
                            sr.Close();
                            return null;
                        }

                        line = line.Substring(pos + 1);
                        pos = line.IndexOf('\"');
                        if (pos == -1) {
                            sr.Close();
                            return null;
                        }

                        line = line.Substring(0, pos);

                        try {
                            return new GameVersion(line);
                        } catch (Exception) {
                            sr.Close();
                            return null;
                        }
                    }

                    sr.Close();
                }
            } catch (Exception) {
                return null;
            }

            return null;
        }
    }
}