using System;
using System.Collections.Generic;

namespace AoC2019
{
    class IntCodeComputer
    {
        private long[] memory;
        private long instructionPointer = 0;
        private long relativeBase = 0;
        private readonly Queue<long> input;

        internal long Output { get; set; } = 0;
        internal bool Halted { get; set; } = false;

        internal IntCodeComputer(long[] memory, Queue<long> input)
        {
            this.memory = memory;
            this.input = input;
        }

        internal void Compute()
        {
            while (true)
            {

                var cmd = GetMemory(instructionPointer) % 100;
                switch (cmd)
                {
                    case 1:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            SetMemory(GetMemory(instructionPointer + 3), noun + verb);
                            instructionPointer += 4;
                            break;
                        }
                    case 2:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            SetMemory(GetMemory(instructionPointer + 3), noun * verb);
                            instructionPointer += 4;
                            break;
                        }

                    case 3:
                        {
                            var location = ((GetMemory(instructionPointer) / 100) % 10) == 2 ? relativeBase : 0;
                            memory[location + memory[instructionPointer + 1]] = input.Dequeue();
                            instructionPointer += 2;
                            break;
                        }

                    case 4:
                        {
                            Output = getInstValue(memory, instructionPointer, true);
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
                            SetMemory(GetMemory(instructionPointer + 3), (noun < verb ? 1 : 0));
                            instructionPointer += 4;
                            break;
                        }

                    case 8:
                        {
                            var noun = getInstValue(memory, instructionPointer, true);
                            var verb = getInstValue(memory, instructionPointer, false);
                            SetMemory(GetMemory(instructionPointer + 3),(noun == verb ? 1 : 0));
                            instructionPointer += 4;
                            break;
                        }
                    case 9:
                        {
                            var location = ((GetMemory(instructionPointer) / 100) % 10) == 2 ? relativeBase : 0;
                            relativeBase += memory[location + memory[instructionPointer + 1]];
                            instructionPointer += 2;
                            break;

                        }
                    case 99:
                        {
                            Halted = true;
                            return;
                        }
                    default:
                        {
                            throw new NotImplementedException(string.Join(",", memory) + " " + instructionPointer);
                        }
                }
            }
        }

        internal void EnqueueInput(long output)
        {
            input.Enqueue(output);
        }

        private long getInstValue(long[] memory, long instrPtr, bool isNounParam)
        {
            long mode = isNounParam ? (GetMemory(instrPtr) / 100) % 10 : GetMemory(instrPtr) / 1000;
            long offset = isNounParam ? 1 : 2;
            return mode switch
            {
                0 => GetMemory(GetMemory(instrPtr + offset)),
                1 => GetMemory(instrPtr + offset),
                2 => GetMemory(relativeBase + GetMemory(instrPtr + offset)),
                _ => throw new NotImplementedException(string.Join(",", memory) + " " + instrPtr)
            };
        }

        private long GetMemory(long pointer)
        {
            if (pointer > memory.Length - 1)
            {
                ResizeMemory(pointer);
            }
            return memory[pointer];
        }

        private void SetMemory(long pointer, long value)
        {
            if (pointer > memory.Length -1)
            {
                ResizeMemory(pointer);
            }
            if (pointer >= 0)
            {
                memory[pointer] = value;
            }
        }

        private void ResizeMemory(long length)
        {
            long[] mem = new long[length+1];
            memory.CopyTo(mem, 0);
            memory = mem;
        }
    }
}
