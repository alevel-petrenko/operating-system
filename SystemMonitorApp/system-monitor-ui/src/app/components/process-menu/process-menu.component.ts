import { Component, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { ProcessInfo } from '../../models/ProcessInfo';
import { ProcessService } from '../../services/process.service';

@Component({
  selector: 'app-process-menu',
  standalone: true,
  imports: [MatIconModule, MatMenuModule, MatButtonModule],
  templateUrl: './process-menu.component.html',
  styleUrl: './process-menu.component.css'
})
export class ProcessMenuComponent {
  @Input() process!: ProcessInfo;

  constructor(private readonly service: ProcessService) { }

  increasePriority() {
    this.service.increasePriority(this.process.id);
  }

  decreasePriority() {
    this.service.decreasePriority(this.process.id);
  }

  killProcess() {
    this.service.killProcess(this.process.id);
  }
}
