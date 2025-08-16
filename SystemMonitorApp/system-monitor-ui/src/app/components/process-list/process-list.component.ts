import { Component } from '@angular/core';
import { ProcessService } from '../../services/process.service';
import { ProcessCardComponent } from '../process-card/process-card.component';
import { ComputerInfo, ComputerService } from '../../services/computer.service';
import { BehaviorSubject, catchError, EMPTY, map, Observable, shareReplay, tap } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { ProcessInfo } from '../../models/ProcessInfo';
import { MatIconModule } from '@angular/material/icon';
import { ProcessSignalRService } from '../../services/process-signalr.service';

@Component({
  selector: 'app-process-list',
  standalone: true,
  imports: [
    ProcessCardComponent,
    AsyncPipe,
    MatIconModule
  ],
  templateUrl: './process-list.component.html',
  styleUrl: './process-list.component.css'
})
export class ProcessListComponent {
  private processesSubject = new BehaviorSubject<ProcessInfo[]>([]);
  public processes$ = this.processesSubject.asObservable();
  public computerName$: Observable<ComputerInfo>;

  public currentPage = 0;
  public pageSize = 16;

  constructor(
    private processService: ProcessService,
    private computerService: ComputerService,
    private signalRService: ProcessSignalRService
  ) {
    this.signalRService.startConnection();

    this.loadProcesses();
    this.computerName$ = this.computerService.getComputerName();

    // SignalR subscription to update processes in real-time
    this.signalRService.onProcessesUpdated((processes) => {
      const sorted = this.sortProcesses(processes);
      this.processesSubject.next(this.addTagsToProcesses(sorted));
    });
  }

  public loadProcesses(): void {
    this.processService.getProcesses()
      .pipe(
        map(processes => this.sortProcesses(processes)),
        tap({
          next: processes => this.addTagsToProcesses(processes)
        }),
        catchError((err) => {
          console.error('Error fetching processes:', err)
          return EMPTY;
        })
      )
      .subscribe(processes => {
        this.processesSubject.next(processes);

        if (processes.length > 0) {
          this.currentPage = 1;
        }
      });
  }

  get paginatedProcesses$(): Observable<ProcessInfo[]> {
    return this.processes$.pipe(
      map(processes => {
        const start = (this.currentPage - 1) * this.pageSize;
        return processes.slice(start, start + this.pageSize);
      })
    );
  }

  get totalPages$(): Observable<number> {
    return this.processes$.pipe(
      map(processes => Math.ceil(processes.length / this.pageSize))
    );
  }

  private sortProcesses(processes: ProcessInfo[]): ProcessInfo[] {
    return processes.sort((a, b) => b.memoryUsageMb - a.memoryUsageMb);
  }

  private addTagsToProcesses(processes: ProcessInfo[]): ProcessInfo[] {
    processes.forEach(process => {
      process.tags = this.knownSystemNames.includes(process.name) ? ['System'] : ['User'];
    });
    return processes;
  }

  private knownSystemNames: string[] = ["Idle", "System", "csrss", "smss", "winlogon", "services", "lsass", "svchost", "fontdrvhost", "dwm"];
}
