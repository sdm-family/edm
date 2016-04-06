﻿namespace Edm.Writer.EPPlus

open Edm
open OfficeOpenXml.Drawing

module Shape =
  let toEPPlusShapeStyle = function
  | ShapeOfAccentBorderCallout1 -> eShapeStyle.AccentBorderCallout1
  | ShapeOfAccentBorderCallout2 -> eShapeStyle.AccentBorderCallout2
  | ShapeOfAccentBorderCallout3 -> eShapeStyle.AccentBorderCallout3
  | ShapeOfAccentCallout1 -> eShapeStyle.AccentCallout1
  | ShapeOfAccentCallout2 -> eShapeStyle.AccentCallout2
  | ShapeOfAccentCallout3 -> eShapeStyle.AccentCallout3
  | ShapeOfActionButtonBackPrevious -> eShapeStyle.ActionButtonBackPrevious
  | ShapeOfActionButtonBeginning -> eShapeStyle.ActionButtonBeginning
  | ShapeOfActionButtonBlank -> eShapeStyle.ActionButtonBlank
  | ShapeOfActionButtonDocument -> eShapeStyle.ActionButtonDocument
  | ShapeOfActionButtonEnd -> eShapeStyle.ActionButtonEnd
  | ShapeOfActionButtonForwardNext -> eShapeStyle.ActionButtonForwardNext
  | ShapeOfActionButtonHelp -> eShapeStyle.ActionButtonHelp
  | ShapeOfActionButtonHome -> eShapeStyle.ActionButtonHome
  | ShapeOfActionButtonInformation -> eShapeStyle.ActionButtonInformation
  | ShapeOfActionButtonMovie -> eShapeStyle.ActionButtonMovie
  | ShapeOfActionButtonReturn -> eShapeStyle.ActionButtonReturn
  | ShapeOfActionButtonSound -> eShapeStyle.ActionButtonSound
  | ShapeOfArc -> eShapeStyle.Arc
  | ShapeOfBentArrow -> eShapeStyle.BentArrow
  | ShapeOfBentConnector2 -> eShapeStyle.BentConnector2
  | ShapeOfBentConnector3 -> eShapeStyle.BentConnector3
  | ShapeOfBentConnector4 -> eShapeStyle.BentConnector4
  | ShapeOfBentConnector5 -> eShapeStyle.BentConnector5
  | ShapeOfBentUpArrow -> eShapeStyle.BentUpArrow
  | ShapeOfBevel -> eShapeStyle.Bevel
  | ShapeOfBlockArc -> eShapeStyle.BlockArc
  | ShapeOfBorderCallout1 -> eShapeStyle.BorderCallout1
  | ShapeOfBorderCallout2 -> eShapeStyle.BorderCallout2
  | ShapeOfBorderCallout3 -> eShapeStyle.BorderCallout3
  | ShapeOfBracePair -> eShapeStyle.BracePair
  | ShapeOfBracketPair -> eShapeStyle.BracketPair
  | ShapeOfCallout1 -> eShapeStyle.Callout1
  | ShapeOfCallout2 -> eShapeStyle.Callout2
  | ShapeOfCallout3 -> eShapeStyle.Callout3
  | ShapeOfCan -> eShapeStyle.Can
  | ShapeOfChartPlus -> eShapeStyle.ChartPlus
  | ShapeOfChartStar -> eShapeStyle.ChartStar
  | ShapeOfChartX -> eShapeStyle.ChartX
  | ShapeOfChevron -> eShapeStyle.Chevron
  | ShapeOfChord -> eShapeStyle.Chord
  | ShapeOfCircularArrow -> eShapeStyle.CircularArrow
  | ShapeOfCloud -> eShapeStyle.Cloud
  | ShapeOfCloudCallout -> eShapeStyle.CloudCallout
  | ShapeOfCorner -> eShapeStyle.Corner
  | ShapeOfCornerTabs -> eShapeStyle.CornerTabs
  | ShapeOfCube -> eShapeStyle.Cube
  | ShapeOfCurvedConnector2 -> eShapeStyle.CurvedConnector2
  | ShapeOfCurvedConnector3 -> eShapeStyle.CurvedConnector3
  | ShapeOfCurvedConnector4 -> eShapeStyle.CurvedConnector4
  | ShapeOfCurvedConnector5 -> eShapeStyle.CurvedConnector5
  | ShapeOfCurvedDownArrow -> eShapeStyle.CurvedDownArrow
  | ShapeOfCurvedLeftArrow -> eShapeStyle.CurvedLeftArrow
  | ShapeOfCurvedRightArrow -> eShapeStyle.CurvedRightArrow
  | ShapeOfCurvedUpArrow -> eShapeStyle.CurvedUpArrow
  | ShapeOfDecagon -> eShapeStyle.Decagon
  | ShapeOfDiagStripe -> eShapeStyle.DiagStripe
  | ShapeOfDiamond -> eShapeStyle.Diamond
  | ShapeOfDodecagon -> eShapeStyle.Dodecagon
  | ShapeOfDonut -> eShapeStyle.Donut
  | ShapeOfDoubleWave -> eShapeStyle.DoubleWave
  | ShapeOfDownArrow -> eShapeStyle.DownArrow
  | ShapeOfDownArrowCallout -> eShapeStyle.DownArrowCallout
  | ShapeOfEllipse -> eShapeStyle.Ellipse
  | ShapeOfEllipseRibbon -> eShapeStyle.EllipseRibbon
  | ShapeOfEllipseRibbon2 -> eShapeStyle.EllipseRibbon2
  | ShapeOfFlowChartAlternateProcess -> eShapeStyle.FlowChartAlternateProcess
  | ShapeOfFlowChartCollate -> eShapeStyle.FlowChartCollate
  | ShapeOfFlowChartConnector -> eShapeStyle.FlowChartConnector
  | ShapeOfFlowChartDecision -> eShapeStyle.FlowChartDecision
  | ShapeOfFlowChartDelay -> eShapeStyle.FlowChartDelay
  | ShapeOfFlowChartDisplay -> eShapeStyle.FlowChartDisplay
  | ShapeOfFlowChartDocument -> eShapeStyle.FlowChartDocument
  | ShapeOfFlowChartExtract -> eShapeStyle.FlowChartExtract
  | ShapeOfFlowChartInputOutput -> eShapeStyle.FlowChartInputOutput
  | ShapeOfFlowChartInternalStorage -> eShapeStyle.FlowChartInternalStorage
  | ShapeOfFlowChartMagneticDisk -> eShapeStyle.FlowChartMagneticDisk
  | ShapeOfFlowChartMagneticDrum -> eShapeStyle.FlowChartMagneticDrum
  | ShapeOfFlowChartMagneticTape -> eShapeStyle.FlowChartMagneticTape
  | ShapeOfFlowChartManualInput -> eShapeStyle.FlowChartManualInput
  | ShapeOfFlowChartManualOperation -> eShapeStyle.FlowChartManualOperation
  | ShapeOfFlowChartMerge -> eShapeStyle.FlowChartMerge
  | ShapeOfFlowChartMultidocument -> eShapeStyle.FlowChartMultidocument
  | ShapeOfFlowChartOfflineStorage -> eShapeStyle.FlowChartOfflineStorage
  | ShapeOfFlowChartOffpageConnector -> eShapeStyle.FlowChartOffpageConnector
  | ShapeOfFlowChartOnlineStorage -> eShapeStyle.FlowChartOnlineStorage
  | ShapeOfFlowChartOr -> eShapeStyle.FlowChartOr
  | ShapeOfFlowChartPredefinedProcess -> eShapeStyle.FlowChartPredefinedProcess
  | ShapeOfFlowChartPreparation -> eShapeStyle.FlowChartPreparation
  | ShapeOfFlowChartProcess -> eShapeStyle.FlowChartProcess
  | ShapeOfFlowChartPunchedCard -> eShapeStyle.FlowChartPunchedCard
  | ShapeOfFlowChartPunchedTape -> eShapeStyle.FlowChartPunchedTape
  | ShapeOfFlowChartSort -> eShapeStyle.FlowChartSort
  | ShapeOfFlowChartSummingJunction -> eShapeStyle.FlowChartSummingJunction
  | ShapeOfFlowChartTerminator -> eShapeStyle.FlowChartTerminator
  | ShapeOfFoldedCorner -> eShapeStyle.FoldedCorner
  | ShapeOfFrame -> eShapeStyle.Frame
  | ShapeOfFunnel -> eShapeStyle.Funnel
  | ShapeOfGear6 -> eShapeStyle.Gear6
  | ShapeOfGear9 -> eShapeStyle.Gear9
  | ShapeOfHalfFrame -> eShapeStyle.HalfFrame
  | ShapeOfHeart -> eShapeStyle.Heart
  | ShapeOfHeptagon -> eShapeStyle.Heptagon
  | ShapeOfHexagon -> eShapeStyle.Hexagon
  | ShapeOfHomePlate -> eShapeStyle.HomePlate
  | ShapeOfHorizontalScroll -> eShapeStyle.HorizontalScroll
  | ShapeOfIrregularSeal1 -> eShapeStyle.IrregularSeal1
  | ShapeOfIrregularSeal2 -> eShapeStyle.IrregularSeal2
  | ShapeOfLeftArrow -> eShapeStyle.LeftArrow
  | ShapeOfLeftArrowCallout -> eShapeStyle.LeftArrowCallout
  | ShapeOfLeftBrace -> eShapeStyle.LeftBrace
  | ShapeOfLeftBracket -> eShapeStyle.LeftBracket
  | ShapeOfLeftCircularArrow -> eShapeStyle.LeftCircularArrow
  | ShapeOfLeftRightArrow -> eShapeStyle.LeftRightArrow
  | ShapeOfLeftRightArrowCallout -> eShapeStyle.LeftRightArrowCallout
  | ShapeOfLeftRightCircularArrow -> eShapeStyle.LeftRightCircularArrow
  | ShapeOfLeftRightRibbon -> eShapeStyle.LeftRightRibbon
  | ShapeOfLeftRightUpArrow -> eShapeStyle.LeftRightUpArrow
  | ShapeOfLeftUpArrow -> eShapeStyle.LeftUpArrow
  | ShapeOfLightningBolt -> eShapeStyle.LightningBolt
  | ShapeOfLine -> eShapeStyle.Line
  | ShapeOfLineInv -> eShapeStyle.LineInv
  | ShapeOfMathDivide -> eShapeStyle.MathDivide
  | ShapeOfMathEqual -> eShapeStyle.MathEqual
  | ShapeOfMathMinus -> eShapeStyle.MathMinus
  | ShapeOfMathMultiply -> eShapeStyle.MathMultiply
  | ShapeOfMathNotEqual -> eShapeStyle.MathNotEqual
  | ShapeOfMathPlus -> eShapeStyle.MathPlus
  | ShapeOfMoon -> eShapeStyle.Moon
  | ShapeOfNonIsoscelesTrapezoid -> eShapeStyle.NonIsoscelesTrapezoid
  | ShapeOfNoSmoking -> eShapeStyle.NoSmoking
  | ShapeOfNotchedRightArrow -> eShapeStyle.NotchedRightArrow
  | ShapeOfOctagon -> eShapeStyle.Octagon
  | ShapeOfParallelogram -> eShapeStyle.Parallelogram
  | ShapeOfPentagon -> eShapeStyle.Pentagon
  | ShapeOfPie -> eShapeStyle.Pie
  | ShapeOfPieWedge -> eShapeStyle.PieWedge
  | ShapeOfPlaque -> eShapeStyle.Plaque
  | ShapeOfPlaqueTabs -> eShapeStyle.PlaqueTabs
  | ShapeOfPlus -> eShapeStyle.Plus
  | ShapeOfQuadArrow -> eShapeStyle.QuadArrow
  | ShapeOfQuadArrowCallout -> eShapeStyle.QuadArrowCallout
  | ShapeOfRect -> eShapeStyle.Rect
  | ShapeOfRibbon -> eShapeStyle.Ribbon
  | ShapeOfRibbon2 -> eShapeStyle.Ribbon2
  | ShapeOfRightArrow -> eShapeStyle.RightArrow
  | ShapeOfRightArrowCallout -> eShapeStyle.RightArrowCallout
  | ShapeOfRightBrace -> eShapeStyle.RightBrace
  | ShapeOfRightBracket -> eShapeStyle.RightBracket
  | ShapeOfRound1Rect -> eShapeStyle.Round1Rect
  | ShapeOfRound2DiagRect -> eShapeStyle.Round2DiagRect
  | ShapeOfRound2SameRect -> eShapeStyle.Round2SameRect
  | ShapeOfRoundRect -> eShapeStyle.RoundRect
  | ShapeOfRtTriangle -> eShapeStyle.RtTriangle
  | ShapeOfSmileyFace -> eShapeStyle.SmileyFace
  | ShapeOfSnip1Rect -> eShapeStyle.Snip1Rect
  | ShapeOfSnip2DiagRect -> eShapeStyle.Snip2DiagRect
  | ShapeOfSnip2SameRect -> eShapeStyle.Snip2SameRect
  | ShapeOfSnipRoundRect -> eShapeStyle.SnipRoundRect
  | ShapeOfSquareTabs -> eShapeStyle.SquareTabs
  | ShapeOfStar10 -> eShapeStyle.Star10
  | ShapeOfStar12 -> eShapeStyle.Star12
  | ShapeOfStar16 -> eShapeStyle.Star16
  | ShapeOfStar24 -> eShapeStyle.Star24
  | ShapeOfStar32 -> eShapeStyle.Star32
  | ShapeOfStar4 -> eShapeStyle.Star4
  | ShapeOfStar5 -> eShapeStyle.Star5
  | ShapeOfStar6 -> eShapeStyle.Star6
  | ShapeOfStar7 -> eShapeStyle.Star7
  | ShapeOfStar8 -> eShapeStyle.Star8
  | ShapeOfStraightConnector1 -> eShapeStyle.StraightConnector1
  | ShapeOfStripedRightArrow -> eShapeStyle.StripedRightArrow
  | ShapeOfSun -> eShapeStyle.Sun
  | ShapeOfSwooshArrow -> eShapeStyle.SwooshArrow
  | ShapeOfTeardrop -> eShapeStyle.Teardrop
  | ShapeOfTrapezoid -> eShapeStyle.Trapezoid
  | ShapeOfTriangle -> eShapeStyle.Triangle
  | ShapeOfUpArrow -> eShapeStyle.UpArrow
  | ShapeOfUpArrowCallout -> eShapeStyle.UpArrowCallout
  | ShapeOfUpDownArrow -> eShapeStyle.UpDownArrow
  | ShapeOfUpDownArrowCallout -> eShapeStyle.UpDownArrowCallout
  | ShapeOfUturnArrow -> eShapeStyle.UturnArrow
  | ShapeOfWave -> eShapeStyle.Wave
  | ShapeOfWedgeEllipseCallout -> eShapeStyle.WedgeEllipseCallout
  | ShapeOfWedgeRectCallout -> eShapeStyle.WedgeRectCallout
  | ShapeOfWedgeRoundRectCallout -> eShapeStyle.WedgeRoundRectCallout
  | ShapeOfVerticalScroll -> eShapeStyle.VerticalScroll