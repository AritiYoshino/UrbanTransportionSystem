# -*- coding: utf-8 -*-
import arcpy
# 设置工作空间，将路径替换为你的地理数据库路径
arcpy.env.workspace = "D:\\arcmap\\成都市.gdb"
# 要素类名称
fc = "公交车站"
# 创建一个字典来存储名称和对应的要素对象列表
name_dict = {}
# 遍历要素类中的所有要素
with arcpy.da.SearchCursor(fc, ["name_st", "OBJECTID"]) as cursor:
    for row in cursor:
        name = row[0]
        oid = row[1]
        if name in name_dict:
            name_dict[name].append(oid)

