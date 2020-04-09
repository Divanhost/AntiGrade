import { ExamType } from './exam-type.model';
import { Employee } from './employee.model';
import { SubjectEmployee } from './subject-employee.model';
import { Group } from './group.model';
import { Work } from './work.model';

export class SubjectDto {
    id: number;
    name: string;
    examTypeId: number;
    examType: ExamType;
    subjectEmployees: SubjectEmployee[];
    works: Work[];
    group: Group;
}
