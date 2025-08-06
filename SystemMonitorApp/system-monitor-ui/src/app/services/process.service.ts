import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataApiService } from '../data-api.service';
import { ProcessInfo } from '../models/ProcessInfo';

@Injectable({
  providedIn: 'root'
})
export class ProcessService {
  constructor(private api: DataApiService) { }

  public getProcesses(): Observable<ProcessInfo[]> {
    return this.api.get<ProcessInfo[]>('process/all'); ;
  }

  public increasePriority(processId: number): void {
    this.api.post(`process/increasePriority/${processId}`, {});
  }

  public decreasePriority(processId: number): void {
    this.api.post(`process/decreasePriority/${processId}`, {});
  }

  public killProcess(processId: number): void {
    this.api.post(`process/kill/${processId}`, {});
  }
}
