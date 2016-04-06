namespace Edm

open System.IO

type NImage = System.Drawing.Image

type ImageBody =
  | ImageObject of NImage
  | ImagePath of FileInfo

type Shape =
  | ShapeOfAccentBorderCallout1
  | ShapeOfAccentBorderCallout2
  | ShapeOfAccentBorderCallout3
  | ShapeOfAccentCallout1
  | ShapeOfAccentCallout2
  | ShapeOfAccentCallout3
  | ShapeOfActionButtonBackPrevious
  | ShapeOfActionButtonBeginning
  | ShapeOfActionButtonBlank
  | ShapeOfActionButtonDocument
  | ShapeOfActionButtonEnd
  | ShapeOfActionButtonForwardNext
  | ShapeOfActionButtonHelp
  | ShapeOfActionButtonHome
  | ShapeOfActionButtonInformation
  | ShapeOfActionButtonMovie
  | ShapeOfActionButtonReturn
  | ShapeOfActionButtonSound
  | ShapeOfArc
  | ShapeOfBentArrow
  | ShapeOfBentConnector2
  | ShapeOfBentConnector3
  | ShapeOfBentConnector4
  | ShapeOfBentConnector5
  | ShapeOfBentUpArrow
  | ShapeOfBevel
  | ShapeOfBlockArc
  | ShapeOfBorderCallout1
  | ShapeOfBorderCallout2
  | ShapeOfBorderCallout3
  | ShapeOfBracePair
  | ShapeOfBracketPair
  | ShapeOfCallout1
  | ShapeOfCallout2
  | ShapeOfCallout3
  | ShapeOfCan
  | ShapeOfChartPlus
  | ShapeOfChartStar
  | ShapeOfChartX
  | ShapeOfChevron
  | ShapeOfChord
  | ShapeOfCircularArrow
  | ShapeOfCloud
  | ShapeOfCloudCallout
  | ShapeOfCorner
  | ShapeOfCornerTabs
  | ShapeOfCube
  | ShapeOfCurvedConnector2
  | ShapeOfCurvedConnector3
  | ShapeOfCurvedConnector4
  | ShapeOfCurvedConnector5
  | ShapeOfCurvedDownArrow
  | ShapeOfCurvedLeftArrow
  | ShapeOfCurvedRightArrow
  | ShapeOfCurvedUpArrow
  | ShapeOfDecagon
  | ShapeOfDiagStripe
  | ShapeOfDiamond
  | ShapeOfDodecagon
  | ShapeOfDonut
  | ShapeOfDoubleWave
  | ShapeOfDownArrow
  | ShapeOfDownArrowCallout
  | ShapeOfEllipse
  | ShapeOfEllipseRibbon
  | ShapeOfEllipseRibbon2
  | ShapeOfFlowChartAlternateProcess
  | ShapeOfFlowChartCollate
  | ShapeOfFlowChartConnector
  | ShapeOfFlowChartDecision
  | ShapeOfFlowChartDelay
  | ShapeOfFlowChartDisplay
  | ShapeOfFlowChartDocument
  | ShapeOfFlowChartExtract
  | ShapeOfFlowChartInputOutput
  | ShapeOfFlowChartInternalStorage
  | ShapeOfFlowChartMagneticDisk
  | ShapeOfFlowChartMagneticDrum
  | ShapeOfFlowChartMagneticTape
  | ShapeOfFlowChartManualInput
  | ShapeOfFlowChartManualOperation
  | ShapeOfFlowChartMerge
  | ShapeOfFlowChartMultidocument
  | ShapeOfFlowChartOfflineStorage
  | ShapeOfFlowChartOffpageConnector
  | ShapeOfFlowChartOnlineStorage
  | ShapeOfFlowChartOr
  | ShapeOfFlowChartPredefinedProcess
  | ShapeOfFlowChartPreparation
  | ShapeOfFlowChartProcess
  | ShapeOfFlowChartPunchedCard
  | ShapeOfFlowChartPunchedTape
  | ShapeOfFlowChartSort
  | ShapeOfFlowChartSummingJunction
  | ShapeOfFlowChartTerminator
  | ShapeOfFoldedCorner
  | ShapeOfFrame
  | ShapeOfFunnel
  | ShapeOfGear6
  | ShapeOfGear9
  | ShapeOfHalfFrame
  | ShapeOfHeart
  | ShapeOfHeptagon
  | ShapeOfHexagon
  | ShapeOfHomePlate
  | ShapeOfHorizontalScroll
  | ShapeOfIrregularSeal1
  | ShapeOfIrregularSeal2
  | ShapeOfLeftArrow
  | ShapeOfLeftArrowCallout
  | ShapeOfLeftBrace
  | ShapeOfLeftBracket
  | ShapeOfLeftCircularArrow
  | ShapeOfLeftRightArrow
  | ShapeOfLeftRightArrowCallout
  | ShapeOfLeftRightCircularArrow
  | ShapeOfLeftRightRibbon
  | ShapeOfLeftRightUpArrow
  | ShapeOfLeftUpArrow
  | ShapeOfLightningBolt
  | ShapeOfLine
  | ShapeOfLineInv
  | ShapeOfMathDivide
  | ShapeOfMathEqual
  | ShapeOfMathMinus
  | ShapeOfMathMultiply
  | ShapeOfMathNotEqual
  | ShapeOfMathPlus
  | ShapeOfMoon
  | ShapeOfNonIsoscelesTrapezoid
  | ShapeOfNoSmoking
  | ShapeOfNotchedRightArrow
  | ShapeOfOctagon
  | ShapeOfParallelogram
  | ShapeOfPentagon
  | ShapeOfPie
  | ShapeOfPieWedge
  | ShapeOfPlaque
  | ShapeOfPlaqueTabs
  | ShapeOfPlus
  | ShapeOfQuadArrow
  | ShapeOfQuadArrowCallout
  | ShapeOfRect
  | ShapeOfRibbon
  | ShapeOfRibbon2
  | ShapeOfRightArrow
  | ShapeOfRightArrowCallout
  | ShapeOfRightBrace
  | ShapeOfRightBracket
  | ShapeOfRound1Rect
  | ShapeOfRound2DiagRect
  | ShapeOfRound2SameRect
  | ShapeOfRoundRect
  | ShapeOfRtTriangle
  | ShapeOfSmileyFace
  | ShapeOfSnip1Rect
  | ShapeOfSnip2DiagRect
  | ShapeOfSnip2SameRect
  | ShapeOfSnipRoundRect
  | ShapeOfSquareTabs
  | ShapeOfStar10
  | ShapeOfStar12
  | ShapeOfStar16
  | ShapeOfStar24
  | ShapeOfStar32
  | ShapeOfStar4
  | ShapeOfStar5
  | ShapeOfStar6
  | ShapeOfStar7
  | ShapeOfStar8
  | ShapeOfStraightConnector1
  | ShapeOfStripedRightArrow
  | ShapeOfSun
  | ShapeOfSwooshArrow
  | ShapeOfTeardrop
  | ShapeOfTrapezoid
  | ShapeOfTriangle
  | ShapeOfUpArrow
  | ShapeOfUpArrowCallout
  | ShapeOfUpDownArrow
  | ShapeOfUpDownArrowCallout
  | ShapeOfUturnArrow
  | ShapeOfWave
  | ShapeOfWedgeEllipseCallout
  | ShapeOfWedgeRectCallout
  | ShapeOfWedgeRoundRectCallout
  | ShapeOfVerticalScroll

type VirticalAlign =
  | VABottom
  | VACenter
  | VADistributed
  | VAJustify
  | VATop

type HorizontalAlign =
  | HALeft
  | HACenter
  | HARight
  | HADistributed
  | HAJustified
  | HAJustifiedLow
  | HAThaiDistributed

type ShapeText = {
  Text: RichText
  VAlign: VirticalAlign
  HAlign: HorizontalAlign
}

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module ShapeText =
  let create text = { Text = text; VAlign = VACenter; HAlign = HACenter }

type ShapeBody = {
  Type: Shape
  Text: ShapeText option
}

[<Measure>]
type pixel

type AddressAndOffset = {
  Address: int
  Offset: int<pixel>
}

type Position =
  | TopLeftPixel of Top:int<pixel> * Left:int<pixel>
  | RowColAndOffsetPixcel of Row:AddressAndOffset * Col:AddressAndOffset

type Size =
  | Percent of int
  | Pixel of Width:int<pixel> * Height:int<pixel>

type DrawingBody =
  | Image of ImageBody
  | Shape of ShapeBody
  // | Chart of ChartBody (* not supported *)

type Drawing = {
  Name: string
  Position: Position
  Size: Size
  Body: DrawingBody
}

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Image =
  let createFromObject name body pos =
    { Name = name; Body = Image (ImageObject body); Position = pos; Size = Percent 100 }

  let cerateFromPath name path pos =
    { Name = name; Body = Image (ImagePath path); Position = pos; Size = Percent 100 }

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Shape =
  let create name shape pos =
    { Name = name; Body = Shape { Type = shape; Text = None }; Position = pos; Size = Percent 100 }

  let createWithText name shape text pos =
    { Name = name; Body = Shape { Type = shape; Text = Some text }; Position = pos; Size = Percent 100 }

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Drawing =
  let updateSize size drawing = { drawing with Drawing.Size = size }
