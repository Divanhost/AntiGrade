import { Component, OnInit } from '@angular/core';
import { Group } from 'src/app/shared/models/group.model';
import { BaseComponent } from 'src/app/shared/classes';
import { GroupService } from 'src/app/core/services/group.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ActivatedRoute } from '@angular/router';
import { SubjectService } from 'src/app/core/services/subject.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent extends BaseComponent implements OnInit {
  subjectId: number;
  groups: Group[] = [];
  modalGroups = [];
  constructor(private readonly groupService: GroupService,
              private readonly subjectService: SubjectService,
              private readonly route: ActivatedRoute) {
    super();
    this.subscriptions.push(
      this.route.params.subscribe(params => this.subjectId = params.id)
    );
  }
  ngOnInit(): void {
    this.getGroups();
  }

  getGroups() {
    if (!this.subjectId) {
      this.loadGroups(false);
    } else {
      this.subscriptions.push(
        this.groupService.getGroupsBySubjectId(this.subjectId).subscribe((response: ResponseModel<Group[]>) => {
          this.groups = response.payload;
        })
      );
    }
  }
  addGroupsToSubject() {
    this.modalGroups = this.modalGroups.filter(x => x.checked);
    const groups = this.modalGroups.map(({ group }) => group);
    this.subscriptions.push(
      this.subjectService.updateSubjectGroups(this.subjectId, groups).subscribe((response: ResponseModel<Group[]>) => {
        this.groups = response.payload;
      })
    );
  }

  loadModalGroups() {
    this.loadGroups(true);
  }

  loadGroups(isModal: boolean) {
    this.subscriptions.push(
      this.groupService.getGroups().subscribe((response: ResponseModel<Group[]>) => {
        if (isModal) {
          response.payload.forEach(item => {
            const ckd = this.groups.some(x => x.id === item.id);
            const modalGroup = { group: item, checked: ckd };
            this.modalGroups.push(modalGroup);
          });
        } else {
          this.groups = response.payload;
        }
        console.log(this.modalGroups);
      })
    );
  }

  deleteGroup(groupId: number) {
    this.subscriptions.push(
      this.groupService.deleteGroup(groupId).subscribe((response: ResponseModel<boolean>) => {
        if (response.payload) {
          this.groups = this.groups.filter(x => x.id !== groupId);
        }
      })
    );
  }
}
