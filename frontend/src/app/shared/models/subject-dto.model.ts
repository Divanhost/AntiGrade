import { ExamType } from './exam-type.model';
import { Employee } from './employee.model';
import { SubjectEmployee } from './subject-employee.model';
import { Group } from './group.model';

export class SubjectDto {
    id: number;
    name: string;
    examTypeId: number;
    examType: ExamType;
    subjectEmployees: SubjectEmployee[];
    group: Group;
}
