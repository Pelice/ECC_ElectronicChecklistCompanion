select *
from 
Airplanes a
left outer join Checklists c on a.AirplaneId = c.AirplaneId
left outer join ChecklistSections s on c.ChecklistId = s.ChecklistId
left outer join ChecklistItems i on i.SectionId = s.SectionId
order by AirplaneId,ChecklistId, SectionOrder,ItemOrder