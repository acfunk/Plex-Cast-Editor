select
	t.id
	,t.tag
	,ti.[index]
from metadata_items mi
join taggings ti
	on mi.id = ti.metadata_item_id
join tags t
	on ti.tag_id = t.id
where mi.id = 1
	and t.tag_type = 6
order by ti.[index];