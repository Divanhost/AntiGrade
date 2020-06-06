import { Component, OnInit } from '@angular/core';
import { Group } from 'src/app/shared/models/group.model';
import { BaseComponent } from 'src/app/shared/classes';
import { GroupService } from 'src/app/core/services/group.service';
import { ResponseModel } from 'src/app/shared/models/response.model';
import { ActivatedRoute } from '@angular/router';
import { SubjectService } from 'src/app/core/services/subject.service';
import { SubjectGroup } from 'src/app/shared/models/subject-group.model';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.scss']
})
export class GroupsComponent extends BaseComponent implements OnInit {
  subjectId: number;
  groups: Group[] = [];
  modalGroups = [];
  subjectGroups: SubjectGroup[] = [];
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
    const groups = this.modalGroups.filter(x => x.checked);
    groups.forEach((element) => {
      this.subjectGroups.push({groupId: element.group.id, subjectId: this.subjectId});
    });
    this.subscriptions.push(
      this.subjectService.updateSubjectGroups(this.subjectId, this.subjectGroups).subscribe((response: ResponseModel<Group[]>) => {
        this.groups = response.payload;
        this.subjectGroups = [];
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
          this.modalGroups = [];
          response.payload.forEach(item => {
            const ckd = this.groups.some(x => x.id === item.id);
            const modalGroup = { group: item, checked: ckd };
            this.modalGroups.push(modalGroup);
          });
        } else {
          this.groups = response.payload;
        }
      })
    );
  }

  deleteGroup(groupId: number) {
    if(this.subjectId) {
      this.groups.forEach((element) => {
        if(element.id !== groupId) {
          this.subjectGroups.push({groupId: element.id, subjectId: this.subjectId});
        }
      });
      this.subscriptions.push(
        this.subjectService.updateSubjectGroups(this.subjectId,this.subjectGroups).subscribe(() => {
          this.groups = this.groups.filter(x=>x.id !== groupId);
          this.subjectGroups =[];
        })
      );
    } else {
      this.subscriptions.push(
        this.groupService.deleteGroup(groupId).subscribe((response: ResponseModel<boolean>) => {
          if (response.payload) {
            this.groups = this.groups.filter(x => x.id !== groupId);
          }
        })
      );
    }
    
  }
}
