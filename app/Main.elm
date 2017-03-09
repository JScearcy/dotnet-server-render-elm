module Main exposing (..)

import Html exposing (Html, programWithFlags, div, text, button)
import Html.Events exposing (onClick)


main : Program Flags Model Msg
main =
    programWithFlags
        { init = init
        , update = update
        , view = view
        , subscriptions = (\_ -> Sub.none)
        }


type alias Model =
    { count : Int
    }


type alias Flags =
    { count : Int
    }


type Msg
    = Increment
    | Decrement


init : Flags -> ( Model, Cmd Msg )
init flags =
    { count = flags.count } ! []


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        Increment ->
            { model | count = model.count + 1 } ! []

        Decrement ->
            { model | count = model.count - 1 } ! []


view : Model -> Html Msg
view model =
    div
        []
        [ button [ onClick Increment ] [ text "+" ]
        , toString model.count |> (++) "Count: " |> text
        , button [ onClick Decrement ] [ text "-" ]
        ]
