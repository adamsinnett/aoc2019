using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2019
{
    class IntCodeComputer
    {
        private int[] memory;
        private int instructionPointer = 0;
        private Queue<int> input;

        internal int output { get; set; } = 0;
        internal bool halted { get; set; } = false; 

        public IntCodeComputer(int[] memory, Queue<int> input)
        {
            this.memory = memory;
            this.input = input;
        }

        public void Compute()
        {
            while (true)
            {

                var cmd = memory[instructionPointer] % 100;
                switch (cmd)
                {
                    case 1:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            memory[memory[instructionPointer + 3]] = noun + verb;
                            instructionPointer += 4;
                            break;
                        }
                    case 2:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            memory[memory[instructionPointer + 3]] = noun * verb;
                            instructionPointer += 4;
                            break;
                        }

                    case 3:
                        {
                            memory[memory[instructionPointer + 1]] = input.Dequeue();
                            instructionPointer += 2;
                            break;
                        }

                    case 4:
                        {
                            output = getInstValue(memory, instructionPointer, true);
                            instructionPointer += 2;
                            return;
                        }

                    case 5:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            instructionPointer = noun == 0 ? instructionPointer + 3 : verb;
                            break;
                        }

                    case 6:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            instructionPointer = noun != 0 ? instructionPointer + 3 : verb;
                            break;
                        }

                    case 7:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            memory[memory[instructionPointer + 3]] = noun < verb ? 1 : 0;
                            instructionPointer += 4;
                            break;
                        }

                    case 8:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            memory[memory[instructionPointer + 3]] = noun == verb ? 1 : 0;
                            instructionPointer += 4;
                            break;
                        }
                    case 99: {
                            halted = true;
                            return;
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }
        }

        internal void EnqueueInput(int output)
        {
            input.Enqueue(output);
        }

        private static int getInstValue(int[] memory, int instrPtr, bool isNounParam)
        {
            int mode = isNounParam ? (memory[instrPtr] / 100) % 10 : memory[instrPtr] / 1000;
            int offset = isNounParam ? 1 : 2;
            return mode != 0 ? memory[instrPtr + offset] : memory[memory[instrPtr + offset]];
        }
    }
}
