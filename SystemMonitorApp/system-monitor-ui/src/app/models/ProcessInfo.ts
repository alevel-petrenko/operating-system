import { ProcessPriority } from "./ProcessPriority";

export interface ProcessInfo {
  id: number;
  name: string;
  threadCount: number;
  memoryUsageMb: number;
  priority: ProcessPriority;
  startTime: Date;
  userName: string;
  tags: string[];
}