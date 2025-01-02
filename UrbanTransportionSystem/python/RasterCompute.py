# -*- coding: utf-8 -*-
import arcpy
from arcpy.sa import *
import sys
import time


# 检查并启用 Spatial Analyst 扩展
arcpy.CheckOutExtension("Spatial")
arcpy.env.overwriteOutput = True
# 设置工作空间
arcpy.env.workspace = sys.argv[6].replace("\\", "//")


# 输入栅格数据的路径
input_raster1 = sys.argv[1].replace("\\", "//")
input_raster2 = sys.argv[2].replace("\\", "//")
# 为输入栅格 1 创建影像金字塔
arcpy.BuildPyramids_management(input_raster1)
# 为输入栅格 2 创建影像金字塔
arcpy.BuildPyramids_management(input_raster2)


# 定义权重
weight1 = float(sys.argv[3])
weight2 = float(sys.argv[4])



# 执行栅格加权和计算
output_raster = (arcpy.Raster(input_raster1) * weight1) + (arcpy.Raster(input_raster2) * weight2)


# 保存结果栅格
output_raster.save(sys.argv[5]+".tif")


# 释放 Spatial Analyst 扩展
arcpy.CheckInExtension("Spatial")
