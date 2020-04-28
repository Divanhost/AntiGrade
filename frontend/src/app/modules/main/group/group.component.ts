import { Component, OnInit, Input } from '@angular/core';
import { BaseComponent } from 'src/app/shared/classes';
import { GroupService } from 'src/app/core/services/group.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { Group } from 'src/app/shared/models/group.model';
import { SubjectDto } from 'src/app/shared/models/subject-dto.model';
import { faUserEdit } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.scss']
})
export class GroupComponent extends BaseComponent implements OnInit {
  faUserEdit = faUserEdit;
  @Input() subjectId: number;
  @Input() subject: SubjectDto = new SubjectDto();
  groups: Group[] = [];
  group: Group;
  constructor(private readonly groupService: GroupService) {
    super();
  }

  ngOnInit(): void {
    this.getGroups();
  }
  getGroups() {
    this.subscriptions.push(
      this.groupService.getGroups().subscribe((responce: ResponseModel<Group[]>) => {
        this.groups = responce.payload;
        if (this.subject.group) {
          this.group = this.groups.find(x => x.id === this.subject.group.id);
        }
      })
    );
  }
  getGroupData() {
    this.group.students.filter(x => x.firstName !== null && x.lastName !== null);
    return this.group;
  }
  updateData(subject: SubjectDto) {
    this.subject = subject;
    this.getGroups();
  }
}
